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

            <b-col md="2">
                <b-card header="FRIENDS">
                    <h6 slot="header" class="text-center mb-0" text-variant="white">FRIENDS</h6>
                    <div v-if="mode == 'edit'" class="com-sm-4" >
                        <b-row style="margin-left: 3%;">
                            <div class="friendCard" v-for="i of friendList" :key="i.id">
                                <td style="width:200px;">{{ i.firstName }} {{ i.lastName }}</td>
                                <td><b-button @click="addParticipant(i.userId, i)" style="margin-right: 5px;"variant="success">></b-button></td>
                            </div>
                        </b-row>
                    </div>
                </b-card>
            </b-col>

            <b-col md="2">
                <b-card header="PARTICIPANTS">
                    <h6 slot="header" class="text-center mb-0" text-variant="white">PARTICIPANTS</h6>
                    <div v-if="mode == 'edit'" class="com-sm-4" >
                        <b-row style="margin-left: 3%;">
                            <div class="friendCard" v-for="i of participantList" :key="i.id">
                                <td style="width:200px;">{{ i.firstName }} {{ i.lastName }}</td>
                                <td><b-button v-if="i.userId != user.userId" @click="removeParticipant(i.userId, i)" style="margin-right: 5px;"variant="success"><</b-button></td>
                                <td><b-button @click="updateBeneficiary(i.userId, i, true)" style="margin-right: 5px;"variant="success">></b-button></td>
                            </div>
                        </b-row>
                    </div>
                </b-card>
            </b-col>

            <b-col md="2">
                <b-card header="BENEFICIARY">
                    <h6 slot="header" class="text-center mb-0" text-variant="white">BENEFICIARY</h6>
                    <div v-if="mode == 'edit'" class="com-sm-4" >
                        <b-row style="margin-left: 3%;">
                            <div class="friendCard" v-for="i of beneficiaryList" :key="i.id">
                                <td style="width:200px;">{{ i.firstName }} {{ i.lastName }}</td>
                                <td><b-button @click="updateBeneficiary(i.userId, i, false)" style="margin-right: 5px;"variant="success"><</b-button></td>
                            </div>
                        </b-row>
                    </div>
                </b-card>
            </b-col>
        </b-row>
</div>
</template>
<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import EventApiService from '../../services/EventApiService';
    import UserApiService from "../../services/UserApiService";
    import AuthService from "../../services/AuthService";
    import ContactApiService from '../../services/ContactApiService';


  export default {
    data() {
      return {
        user: {},
        event: {},
        mode: null,
        eventId: 0,
        friendList: [],
        participantList: [],
        beneficiaryList: [],
        selected: [],
        errors: [],
        showDismissibleAlert: false
      }
    },
    

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.mode = this.$route.params.mode;
        this.eventId = this.$route.params.id;
        
        await this.refreshfriendList();
        await this.refreshParticipantList();

        if(this.mode == 'edit'){
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.event = await this.executeAsyncRequest(() => EventApiService.getEventByIdAsync(this.eventId));
                this.event.dates = this.event.dates.slice(0, 10);
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/events');
            }
        }
    },

    methods: {

        ...mapActions(['executeAsyncRequest']),

        async addParticipant(userId, element){
            var participant = {};
            participant.eventId = this.eventId;
            participant.userId = userId;
            participant.participantType = false;
            participant.invitation = false;

            await ParticipantApiService.createParticipantAsync(participant);
            this.removeElement(this.friendList, element);
            this.participantList.push(element);
        },

        async removeParticipant(userId, element){
            await ParticipantApiService.deleteParticipantAsync(userId, this.eventId);
            this.removeElement(this.participantList, element);
            this.friendList.push(element);
        },

        async updateBeneficiary(userId, element, benef){
            var participant = await ParticipantApiService.getParticipant(userId, this.eventId);

            participant.participantType = benef;

            await ParticipantApiService.updateParticipantAsync(participant);
            if (benef == true)
            {
                this.removeElement(this.participantList, element);
                this.beneficiaryList.push(element);
            }
            else
            {
                this.removeElement(this.beneficiaryList, element);
                this.participantList.push(element);
            }
        },

        async refreshfriendList() {
            this.friendList = await ContactApiService.getFriendsAsync(this.user.userId);
        },
        
        async refreshParticipantList(){
            var aux = await ParticipantApiService.getParticipantInvitedListAsync(this.user.userId, this.eventId);
            var user;
            for (var i = 0; i < aux.length; i++)
            {
                user = await UserApiService.getUserByIdAsync(aux[i].userId);
                this.participantList.push(user);
                if (aux[i].participantType == 1)
                {
                    this.beneficiaryList.push(user);
                    this.removeElement(this.participantList, user);
                }
                this.removeElement(this.friendList, user);
            }
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
                        await this.executeAsyncRequest(() => EventApiService.createEventAsync(this.event));
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
        }
    }
};
</script>

<style lang="less">
.row {
    margin-top: 10px;
}

.friendCard {
    height: 58px;
    width: 200px;
    margin-top: 7%;
}

</style>