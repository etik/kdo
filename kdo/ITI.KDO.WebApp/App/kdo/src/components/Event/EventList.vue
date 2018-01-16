<template>
    <div class="container">
        <div class="page-header">
            <h1>Events</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`events/create`">Add an event</router-link>
            </div>
        </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Event Name</th>
                    <th>Description</th>
                    <th>Date</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="eventList.length == 0">
                    <td colspan="7" class="text-center">Event</td>
                </tr>

                <tr v-for="i of eventList" :key="i.eventId">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.descriptions }}</td>
                    <td>{{ i.dates }}</td>
                    <td>
                    <b-button-group v-if = "isCreator(i.userId) == true">
                        <b-button :to="`events/edit/${i.eventId}`">Edit</b-button>
                        <b-button :to="`events/view/${i.eventId}`">View</b-button>
                        <b-button @click="deleteEvent(i.eventId) ">Remove</b-button>
                    </b-button-group>

                    <b-button variant="primary" v-if = "isCreator(i.userId) == false" @click="changeRoute()">Suggest another date</b-button>

                    <b-button-group v-if = "isCreator(i.userId) == false">
                        <b-button @click = "quitEvent(i.eventId)">Quit event</b-button>
                    </b-button-group>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import ParticipantApiService from '../../services/ParticipantApiService';
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';
    
  export default {
    data() {
        return {
            user: {},
            eventList: [],
            eventSuggest: {}
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },

    methods: {
        ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

        async refreshList() {
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            for (var i = 0; i < this.eventList.length; i++){
                this.eventList[i].dates = this.eventList[i].dates.slice(0, 10);
            }
        },
        async deleteEvent(eventId) {
            try {
                
                await EventApiService.deleteEventAsync(eventId);
                await this.refreshList();
            }
            catch(error) {

            }
        },

        isCreator(creatorId){
            if(this.user.userId == creatorId){
                return true;
            }else {
                return false;
            }
        },

        async quitEvent(eventId){
            await ParticipantApiService.deleteParticipantAsync(this.user.userId, eventId);
            await this.refreshList();
        },

        async onSubmit(e, eventId){
            e.preventDefault();

            try {
                this.eventSuggest.eventId = eventId;
                this.eventSuggest.userId = this.user.userId;
                await EventApiService.createEventSuggestAsync(this.eventSuggest);
            }
            catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
            }
        },

        changeRoute(){
            this.$router.replace("events/suggest/calendar");
        }
  }
  };
</script>

<style lang="less">

</style>