<template>
    <div class="container">    
      <div class="page-header">
            <h1 v-if="mode == 'create'">Create a event</h1>
            <h1 v-else>Edit your event</h1>
        </div>


        <div class="row">
        <div class="com-sm-4">
        <b-card>
        <h1 v-if="mode == 'create'">Create a event</h1>
        <h1 v-else>Edit your event</h1>
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
                <b-form-textarea asp-for="Description" class="form-control" v-model="event.Description">
                <span asp-validation-for="Description"></span>
                </b-form-textarea>
            </b-form-group>
            </b-col>

            <b-col md="12">
            <b-form-group label="Date">
                <b-form-input type="date" asp-for="Date" class="form-control" v-model="event.date">
                <span asp-validation-for="Date"></span>
                </b-form-input>
            </b-form-group>
            </b-col>
            
            <b-col md="6">
            <b-button type="submit" variant="primary">Submit</b-button>
            </b-col>
        </b-form>
        </b-card>
        </div>

        <div class="com-sm-4">
        <b-col md="12">
        <b-card>
            <h5 class="mt-3">Your friends</h5>
            <div v-for="i of friendList" :key="i.id">
            <input type="checkbox" :id="i.id" name="flavour1" :value="i.firstName" v-model="selected" :for="i.id">{{i.firstName}} {{i.lastName}}
            </div>
            <b-button @click="addParticipant()" variant="success">Success</b-button>
        </b-card>
        </b-col>
        </div>

        <span>Noms cochés : {{ selected }}</span>


        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>FirstName</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="friendList.length == 0">
                    <td colspan="7" class="text-center">No Contact found</td>
                </tr>

                <tr v-for="i of friendList">
                    <td>{{ i.firstName }}</td>
                </tr>
            </tbody>
        </table>

        <div v-if="mode == 'edit'" class="com-sm-4">
        <b-col md="12">
            <h1> Déja Participant</h1>
           <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Participant</th>
                    <th>userId</th>
                    <th>eventId</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="participantList.length == 0">
                    <td colspan="7" class="text-center">Participant</td>
                </tr>

                <tr v-for="i of participantList">
                    <td>{{ i.ParticipantType }}</td>
                    <td>{{ i.userId }}</td>
                    <td>{{ i.eventId }}</td>
                </tr>
            </tbody>
        </table>
        </b-col>
        </div>


        </div>
        <!--form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group">
                <label class="required">Event Name:</label>
                <input type="text" v-model="event.eventName" class="form-control" required>
            </div>

            <div class="form-group">
                <label class="">Description</label>
                <textarea type="text" v-model="event.descriptions" class="form-control" ></textarea>
            </div>

            </div-->
            <!--div class="form-group">
                <label>Participants</label>
                <b-dropdown right text="Menu">
                    <tr v-for="i of eventList">
                        <b-dropdown-item>Item 1</b-dropdown-item>
                    </tr>
                </b-dropdown>
                <input type="text" v-model="item.friendsId" class="form-control">
            </div>

            <div class="form-group">
                <label class="required">Date</label>
                <input type="text" v-model="item.date" class="form-control" required>
            </div-->
            <!--div class="form-group row">
            <label for="example-datetime-local-input" class="">Date and time</label>
            <div class="col-10">
                <input class="form-control" type="datetime-local" v-model="event.date" value="2011-08-19T13:45:00" id="example-datetime-local-input" >
            </div>
            </div>

             <div>
                <h5 class="mt-3">Your friends</h5>
                <b-form-checkbox-group stacked v-model="selected" name="flavour2" :options="options">
                </b-form-checkbox-group>
              </div>

            <button type="submit" class="btn btn-primary">Sauvegarder</button>
        </form>
    </div-->
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
        user:{},
        event:{},
        mode: null,
        eventId: null,
        participant:{},
        participantList: [],
        friendList:[],
        selected:[],
        errors: [],
        options: [
        {text: 'Xavier F', value: 'Xavier F'},
        {text: 'Eric H', value: 'Eric H'},
        {text: 'Charles O', value: 'Charles O'},
        {text: 'Loic D', value: 'Loic D'}
        ]
      }
    },
    

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.mode = this.$route.params.mode;
        this.eventId = this.$route.params.id;
        

       await this.refreshParticipantList();
       await this.refreshfriendList();
       await this.addParticipant();

        if(this.mode == 'edit'){
                try {
                    // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                    // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                    this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));
                }
                catch(error) {
                    // So if an exception occurred, we redirect the user to the students list.
                    this.$router.replace('/events');
                }
            }
    },

    methods: {
      ...mapActions(['executeAsyncRequest']),
       async refreshParticipantList(){
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
      },
      async refreshfriendList() {
        this.friendList = await ContactApiService.getFriendsAsync(this.user.userId);
      },
      async addParticipant() {
          this.selected = await ParticipantApiService.createParticipantAsync();
      },
      async onSubmit(e){
        e.preventDefault();

        var errors = [];

        if(!this.event.eventName) errors.push("Event Name");
        //if(!this.event.participants) errors.push("Participants");
        //if(!this.event.date) errors.push("Date");
        //if(!this.event.friendsId) errors.push("friends Id");

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
      }
    }
  };
</script>

<style lang="less">
.row {
    margin-top: 5%;
}

</style>