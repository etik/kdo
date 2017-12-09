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
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="notificationList.length == 0">
                    <td colspan="7" class="text-center">No notification</td>
                </tr>

                <tr v-for="i of notificationList">
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.senderEmail }}</td>
                    <td>{{ i.recipientsEmail }}</td>
                    <td>
                        <button @click="responseInvitation('yes', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-primary">Accept</button>
                        <button @click="responseInvitation('no', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-primary">Decline</button>
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
                notificationList: [],
                model: {}
            };
        },

        async mounted() {
            var userEmail = AuthService.emailUser();
            this.user = await UserApiService.getUserAsync(userEmail);
            await this.refreshList();
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async refreshList() {
                this.notificationList = await NotificationApiService.getNotificationListAsync(this.user.userId);
            },

            async responseInvitation(response, firstEmail, secondEmail, notificationId){
                if(response == 'yes'){
                    try {
                        this.model.senderEmail = firstEmail;
                        this.model.recipientsEmail = secondEmail;
                        
                        await ContactApiService.setContactInvitation(this.model);
                    } catch (error) {
                        
                    }
                }else{
                    try {
                        await ContactApiService.deleteContactAsync(notificationId);
                    } catch (error) {
                        
                    }
                }
                await this.refreshList();
            }
        }
    };
</script>

<style lang="less">

</style>