<template>
    <div>
        <div class="text-center" style="padding: 50px">
            <button type="button" @click="sendEmail('OccasionInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Send Occasion Invitation</button>
            <button type="button" @click="sendEmail('FriendInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-facebook-square" aria-hidden="true"></i> Send Friends Invitation</button>
            <router-link :to="`notification/${this.receivePerson.userId}`">Notification</router-link>
            <input v-model="this.recipientsEmail" placeholder="Find friend's email">
            <button type="submit" class="btn btn-primary">Send friend request</button>
        </div>
    </div>
</template>

<script>
import AuthService from "../../services/AuthService";
import UserApiService from "../../services/UserApiService"
import NotificationApiService from "../../services/NotificationApiService"
import Vue from "vue";
import $ from "jquery";
import Vuex from 'vuex';

export default {
    data() {
        return {
            endpoint: null,
            user:{},
            receivePerson: {},
            recipientsEmail: null,
            model:{},
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        
        this.receivePerson = await UserApiService.getUserAsync("minh@gmail.com");
        console.log(this.receivePerson.userId);
    },

    methods: {
        sendEmail(mailType){
            AuthService.sendEmail(mailType);
        },

        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(this.friendEmail) errors.push("Event Name");

            this.errors = errors;

            if(errors.length == 0) {
                try {
                    this.model.userId = this.user.userId;
                    this.model.recipientsEmail = this.recipientsEmail;
                    this.model.senderEmail = this.user.email;
                    this.model.descriptions = "hello";
                    this.model.inviteAccept = false;
                    
                    await this.executeAsyncRequest(() => NotificationApiService.createNotificationAsync(this.model));
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