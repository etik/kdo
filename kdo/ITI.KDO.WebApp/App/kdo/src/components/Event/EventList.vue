<template>
    <div class="container">
      <div class="page-header">
            <h1>Events</h1>
      </div>

      <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`events/create`"><i class="glyphicon glyphicon-plus"></i>Add an event</router-link>
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

                <tr v-for="i of eventList">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.descriptions }}</td>
                    <td>{{ i.dates }}</td>
                    <td>
                    <b-button-group v-if = "isCreator(i.userId) == true">
                        <b-button :to="`events/edit/${i.eventId}`">Edit</b-button>
                        <b-button :to="`events/view/${i.eventId}`">View</b-button>
                        <b-button @click="deleteEvent(i.eventId) ">Remove</b-button>
                    </b-button-group>

                    <b-button-group v-if = "isCreator(i.userId) == false">
                        <b-button :to="`events/edit/${i.eventId}`">Suggest another date</b-button>
                        <b-button :to="`events/dateSuggest/${i.eventId}`">View</b-button>
                        <b-button @click="quitEvent(i.eventId) ">Quit event</b-button>
                    </b-button-group>

                    <td>
                        <!--button @click="deleteEvent(i.eventId)"  class="btn btn-primary">Remove</button>
                        <router-link :to="`events/edit/${i.eventId}`">Edit event</router-link>
                        <router-link :to="`events/view/${i.eventId}`">View</router-link-->

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
            eventList: []
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
        }
  }
  };
</script>

<style lang="less">

</style>