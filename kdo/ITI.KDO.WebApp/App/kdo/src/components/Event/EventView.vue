<template>
    <div class="container">
      <div class="page-header">
            <h1>Events</h1>
      </div>


      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Event Name</th>
                    <th>Description</th>
                    <th>Date</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="eventList.length == 0">
                    <td colspan="7" class="text-center">Event</td>
                </tr>

                <tr v-for="i of eventList">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.description}}</td>
                    <td>{{ i.date }}</td>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
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
      }

  }
  };
</script>

<style lang="less">

</style>