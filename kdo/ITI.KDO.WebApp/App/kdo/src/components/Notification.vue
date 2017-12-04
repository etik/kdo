<template>
    <div class="container">
      <div class="page-header">
            <h1>Notifications List</h1>
      </div>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Notification Id</th>
                    <th>User Id</th>
                    <th>Recipients Email</th>
                    <th>Sender Email</th>
                    <th>Description</th>
                    <th>Invite Accept</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="presentList.length == 0">
                    <td colspan="7" class="text-center">We-want-a-present!!!</td>
                </tr>

                <tr v-for="i of notificationList">
                    <td>{{ i.notificationId }}</td>
                    <td>{{ i.userId }}</td>
                    <td>{{ i.recipientsEmail }}</td>
                    <td>{{ i.senderEmail }}</td>
                    <td>{{ i.descriptions }}</td>
                    <td>{{ i.inviteAccept }}</td>
                    <td>
                        <button @click="deletePresent(i.notificationId)" class="btn btn-primary">Delete</button>
                        <router-link :to="`presents/edit/${i.presentId}`">Edit Present</router-link>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../services/AuthService";
    import PresentApiService from '../services/PresentApiService';
    import UserApiService from '../services/UserApiService';
    import Notification from '../services/NotificationApiService';

  export default {
    data() {
        return {
            user: {},
            notificationList: [],
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
            this.presentList = await notificationList.getNotificationListAsync(this.user.userId);
      },

      async deletePresent(notificationId) {
          try {
              await PresentApiService.deletePresentAsync(notificationId);
              await this.refreshList();
          }
          catch(error) {

          }
      }
  }
  };
</script>

<style lang="less">

</style>