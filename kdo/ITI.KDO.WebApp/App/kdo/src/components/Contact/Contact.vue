<template>
<div>
    <section>
    <div class="title">
        <h1>CONTACTS</h1>
    </div>
    </section>

    <b-row>
    <b-col md="3">
    </b-col>
    <b-col md="7">
        <b-card-group deck class="mb-2">
        <b-card
            img-src="https://www.walldevil.com/wallpapers/a18/thumb/background-lemons-web-nvstormygetaway-images-profile-naver.jpg"
            img-alt="Image"
            header-text-variant="white"
            header-bg-variant="dark"
            img-top
            tag="article"
            v-for="j in 6" v-if="friendsList[j - 1] != null" cols="2"
                    text-variant="black"
                    :header="friendsList[j - 1].firstName + ' ' + friendsList[j - 1].lastName"
                    class="text-center"
                    style="max-width: 128px;">
            </b-card>
            </b-card-group>

    </b-col>
    <b-col md="2">
    </b-col>
    </b-row>
        <b-row style="margin-top: 2%;">
    <b-col md="3">
    </b-col>
    <b-col md="7">
            <form @submit="onSubmit($event)">
            <input v-model="recipientsEmail" placeholder="Find friend's email">
            <!--button type="submit" class="btn btn-primary">Send friend request</button-->

            <b-btn type="submit" @click="showDismissibleAlert=true" variant="info" class="m-1">
            Send friend request
            </b-btn>
            </form>
    </b-col>
    <b-col md="2">
    </b-col>
    </b-row>
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
            listFacebookFriends: {},
            friendsList: [],
            nbline: 0,
            showDismissibleAlert: false
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
            this.friendsList = await ContactApiService.getFriendsAsync(this.user.userId);
            this.nbline = Math.trunc((this.friendsList.length + 5) / 6);
        },

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
<style lang="less">
</style>