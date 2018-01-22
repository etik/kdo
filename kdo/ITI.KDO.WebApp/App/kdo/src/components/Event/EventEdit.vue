<template>
<div>
    <section>
    <div class="title">
        <h1 v-if="mode == 'create'">CREATE AN EVENT</h1>
        <h1 v-else>EDIT AN EVENT</h1>
    </div>
    </section>

        <b-row>
            <b-col md="5" style="margin-left: 1%;">

                <b-card header="EVENT" >
                 <h6 slot="header" class="text-center mb-0" text-variant="white">EVENT</h6>
                    <b-form  @submit="onSubmit($event)">
                        <div class="alert alert-danger" v-if="errors.length > 0">
                        <b>Les champs suivants semblent invalides : </b>

                        <ul>
                        <li v-for="e of errors">{{e}}</li>
                        </ul>
                        </div>
                        <b-col md="12">
                        <b-form-group label="Event Name:" required>
                        <b-form-input asp-for="EventName" class="form-control" v-model="event.eventName" required>
                        <span asp-validation-for="EventName"></span>
                        </b-form-input>
                        </b-form-group>
                        </b-col>

                        <b-col md="12">
                        <b-form-group label="Description:">
                        <b-form-textarea asp-for="Description" class="form-control" v-model="event.descriptions">
                        <span asp-validation-for="Description"></span>
                        </b-form-textarea>
                        </b-form-group>
                        </b-col>

                        <b-col md="12">
                        <b-form-group label="Date">
                        <b-form-input type="date" asp-for="Date" class="form-control" v-model="event.dates">
                        <span asp-validation-for="Date"></span>
                        </b-form-input>
                        </b-form-group>
                        </b-col>

                        <b-col md="6">
                        <b-button type="submit" variant="primary">Submit</b-button>
                        </b-col>
                    </b-form>
                </b-card>
                </b-col>
            <b-col md="6">
                <b-card header="FRIENDS">
                <b-alert variant="success" dismissible :show="showDismissibleAlert"
                                        @dismissed="showDismissibleAlert=false">
                                        You add a friend as participant !
                </b-alert>
                    <h6 slot="header" class="text-center mb-0" text-variant="white">FRIENDS</h6>
                    <div v-if="mode == 'edit'" class="com-sm-4" >
                        <b-row style="margin-left: 3%;">
                            <div v-for="i of friendList" :key="i.id">
                                <b-col md="3">
                                <b-card  style="width: 75%;margin-top: 7%;">
                                <td>{{ i.firstName }} {{ i.lastName }}</td>
                                <td><b-button @click="addParticipant(i.userId, i),showDismissibleAlert=true" style="margin-left: 92%;"variant="success">+</b-button></td>
                                </b-card>
                                </b-col>
                            </div>
                        </b-row>
                    </div>
                </b-card>
            </b-col>
        </b-row>
        <b-card title="Event">
            <h4>Chose a picture for your Event</h4>
            <div class="input-group" style="width : 25%;">
                <label class="input-group-btn">
                    <span class="btn btn-primary btn-file">
                        Browse
                        <input type="file" @change="onFileChange" style="display: none;" multiple>
                    </span>
                </label>
                <input type="text" class="form-control" v-model="sendImage.name" readonly>
            </div>
        </b-card>
</div>
</template>
<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import EventApiService from '../../services/EventApiService';
    import UserApiService from "../../services/UserApiService";
    import AuthService from "../../services/AuthService";
    import ContactApiService from '../../services/ContactApiService';
    import FileApiService from '../../services/FileApiService';


  export default {
    data() {
      return {
        user: {},
        event: {},
        mode: null,
        eventId: 0,
        participant: {},
        participantList: [],
        friendList: [],
        selected: [],
        image: '',
        sendImage: '',
        data : new FormData(),
        errors: [],
        showDismissibleAlert: false
      }
    },
    

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.mode = this.$route.params.mode;
        console.log(this.mode);
        this.eventId = this.$route.params.id;
        
        await this.refreshParticipantList();
        await this.refreshfriendList();

        if(this.mode == 'edit')
        {
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.event = await this.executeAsyncRequest(() => EventApiService.getEventByIdAsync(this.eventId));
                this.event.dates = this.event.dates.slice(0, 10);
                if(this.data != null)
                {
                    this.sendItemImage = await FileApiService
                        .updateFileAsync(this.data, this.event.eventId)
                        .then( () => { FileApiService.typeOfPicture(2, this.event.eventId)});
                }
                
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/events');
            }
        }
    },

    methods: {

        ...mapActions(['executeAsyncRequest']),

        onFileChange(e) {
            console.log("in OnFileChange");             
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.createImage(files[0]);
            this.$router.replace('/events/create');      
        },
        createImage(file) {
            console.log("in CreateImage");   
            var image = new Image();
            var reader = new FileReader();
            var vm = this;

            this.data.append('files', file);
            this.sendImage = file;
            console.log("sendImage : " + this.sendImage);
            console.log("file : " + file);
            reader.onload = (e) => {
                vm.image = e.target.result;
            };
            reader.readAsDataURL(file);
        },
        async removeImage(e) {
            console.log("in RemoveImage");   
            e.preventDefault();
            this.image = '';
            this.file = '';
            this.data.append('files',  this.file);
            if(this.data != null)
            {
                console.log("this.image is not null");
                console.log("this.data : " + this.data);
                console.log("this.item.userId : " + this.item.userId);
                await FileApiService.deletePicture( 2, this.event.eventId );
                    
            }
            this.refresh();
            this.$router.replace('userProfil');           
        },

        async refreshParticipantList(){
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
        }, 

        async addParticipant(userId, element){
            var participant = {};
            participant.eventId = this.eventId;
            participant.userId = userId;
            participant.participantType = false;
            participant.invitation = false;

            await ParticipantApiService.createParticipantAsync(participant);
            this.removeElement(this.friendList, element);
        },

        async refreshfriendList() {
            this.friendList = await ContactApiService.getFriendsAsync(this.user.userId);
        },
        async inviteParticipant() {
            this.selected = await ParticipantApiService.createParticipantAsync();
        },
        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(!this.event.eventName) errors.push("Event Name");

            this.errors = errors;

            if(errors.length == 0) {
                try {
                    if(this.mode == 'create') {
                        this.event.userId = this.user.userId;
                        console.log("hello");
                        this.event.eventId = await this.executeAsyncRequest(() => EventApiService.createEventAsync(this.event));      
                        if(this.data != null)
                        {
                            console.log("this.image is not null");
                            console.log("this.data : " + this.data);
                            console.log("this.event.eventId: " + this.event.eventId);
                            this.sendItemImage = await FileApiService
                                .updateFileAsync(this.data, this.event.eventId)
                                .then( () => { FileApiService.typeOfPicture(2, this.event.eventId)});
                        }
                    }
                    else {
                        await this.executeAsyncRequest(() => EventApiService.updateEventAsync(this.event)); 
                    }
                    this.$router.replace('/events');
                }
                catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            }
        },

        removeElement(list, i){
            list.splice(list.indexOf(i), 1);
            console.log(list);
        }
    }
};
</script>

<style lang="less">
.row {
    margin-top: 10px;
}

</style>