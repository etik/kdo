<template>
<div>
    <div class="row">
    <b-col class="com-sm-5" v-for="i of eventList">
    <b-card title="Your Event">
    <p class="card-text">
    {{i.eventName}}
    </p>
    </b-card>
    </b-col>
    </div>


</div>
</template>

<script>
import { mapActions } from 'vuex';
import AuthService from "../services/AuthService";
import EventApiService from '../services/EventApiService';
import UserApiService from '../services/UserApiService';

export default {
    data () {
        return {
            userEmail: null,
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
      }
  }
  };
</script>

<style>
</style>