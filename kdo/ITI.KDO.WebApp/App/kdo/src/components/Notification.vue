<template>
    <div class="container">
      <div class="page-header">
            <h1>Notifications List</h1>
      </div>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Notification Id</th>
                    <th>From</th>
                    <th>To</th>
                    <th>Description</th>
                    <th>Invite Accept</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="notificationList.length == 0">
                    <td colspan="7" class="text-center">No notification</td>
                </tr>

                <tr v-for="i of notificationList">
                    <td>{{ i.notificationId }}</td>
                    <td>{{ i.senderEmail }}</td>
                    <td>{{ i.recipientsEmail }}</td>
                    <td>{{ i.descriptions }}</td>
                    <td>{{ i.inviteAccept }}</td>
                    <td>
                        <button @click="deleteNotification(i.notificationId)" class="btn btn-primary">Delete</button>
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
    import NotificationApiService from '../services/NotificationApiService';

  export default {
    data() {
        return {
            user: {},
            id: null,
            notificationList: [],
        };
    },

    async mounted() {
        this.id = this.$route.params.id;
        console.log(this.id);
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.notificationList = await NotificationApiService.getNotificationListAsync(this.id);
      },

      async deleteNotification(notificationId) {
          try {
              await NotificationApiService.deleteNotificationAsync(notificationId);
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