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
                        <button @click="responseInvitation('yes', i.senderEmail, i.recipientsEmail, i.notificationId)" class="btn btn-primary">Accept</button>
                        <button @click="responseInvitation('no')" class="btn btn-primary">Decline</button>
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
    import ContactApiService from '../services/ContactApiService';

    export default {
        data() {
            return {
                user: {},
                id: null,
                notificationList: [],
                model: {}
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
            ...mapActions(['executeAsyncRequest']),

            async refreshList() {
                this.notificationList = await NotificationApiService.getNotificationListAsync(this.id);
            },

            async responseInvitation(response, firstEmail, secondEmail, notificationId){
                if(response == 'yes'){
                    try {
                        this.model.firstEmail = firstEmail;
                        this.model.secondEmail = secondEmail;

                        console.log(this.model.firstEmail);
                        console.log(this.model.secondEmail);
                        console.log(notificationId);

                        await ContactApiService.createContactAsync(this.model);
                    } catch (error) {
                        
                    }
                }
                await NotificationApiService.deleteNotificationAsync(notificationId);
                await this.refreshList();
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