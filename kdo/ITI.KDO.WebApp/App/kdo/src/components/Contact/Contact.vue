<template>
    <div>
        <div class="text-center" style="padding: 50px">
            <button type="button" @click="sendEmail('OccasionInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Send Occasion Invitation</button>
            <button type="button" @click="sendEmail('FriendInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-facebook-square" aria-hidden="true"></i> Send Friends Invitation</button>
            <router-link :to="`notification/${this.user.userId}`">Notification</router-link>
            <form @submit="onSubmit($event)">
                <input v-model="recipientsEmail" placeholder="Find friend's email">
                <button type="submit" class="btn btn-primary">Send friend request</button>
            </form>
            <router-link :to="`contacts`">Contact List</router-link>
            <router-link :to="`facebookContacts`">Facebook Contact List</router-link>
        </div>
    </div>
</template>

<script>
    import Vue from "vue";
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import NotificationApiService from "../../services/NotificationApiService";
    import ContactApiService from "../../services/ContactApiService";
    

export default {
    data() {
        return {
            endpoint: null,
            user:{},
            receivePerson: {},
            recipientsEmail: null,
            model:{},
            listFacebookFriends: {}
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        sendEmail(mailType){
            AuthService.sendEmail(mailType);
        },

        async getFacebookFriends(){
            try {
                    this.listFacebookFriends = await FacebookApiService.getFacebookFriends();
                }
                catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            
        },

        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(this.friendEmail) errors.push("Friend Email");

            this.errors = errors;
            if(errors.length == 0) {
                try {
                    var recipientsUser = await UserApiService.getUserAsync(this.recipientsEmail);

                    this.model.userId = this.user.userId;
                    this.model.friendId = recipientsUser.userId;
                    this.model.invitaion = false;

                    await this.executeAsyncRequest(() => ContactApiService.createContactAsync(this.model));
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