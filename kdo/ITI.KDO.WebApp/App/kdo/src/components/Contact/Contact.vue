<template>
    <div>
        <b-card-group deck v-for="i in nbline" :key="i" class="mb-2">
            <b-card v-for="j in 6" :key="j" v-if="contactList[j - 1] != null" cols="2"
                    text-variant="black"
                    :header="contactList[j - 1].firstName"
                    class="text-center"
                    style="max-width: 128px;">
                <p class="card-text">{{contactList[j-1]}}</p>
            </b-card>
        </b-card-group>

        <div class="text-center" style="padding: 50px">
            <button type="button" @click="sendEmail('OccasionInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Send Occasion Invitation</button>
            <button type="button" @click="sendEmail('FriendInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-facebook-square" aria-hidden="true"></i> Send Friends Invitation</button>
            <router-link :to="`notification/${this.user.userId}`">Notification</router-link>
            <form @submit="onSubmit($event)">
                <input v-model="recipientsEmail" placeholder="Find friend's email">
                <b-btn type="submit" class="btn btn-primary" v-b-modal.modal>Send friend request</b-btn>
                <b-modal id="modal" title="Sending Email">
                    <p class="my-4">Email sent !</p>
                </b-modal>
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
            listFacebookFriends: {},
            contactList: [],
            nbline: 0
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
            this.contactList = await ContactApiService.getContactListAsync(this.user.userId);
            this.nbline = Math.trunc((this.contactList.length + 5) / 6);
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