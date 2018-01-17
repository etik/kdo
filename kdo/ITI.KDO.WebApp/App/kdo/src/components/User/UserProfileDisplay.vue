<template>
    <div class="container">
      <div class="page-header">
            <h1>Profil de {{ profil.firstName }} {{ profil.lastName }}</h1>
      </div>
      <p>First Name: {{ profil.firstName }}</p>
      <p>Last Name: {{ profil.lastName }}</p>
      <p>Email: {{ profil.email }}</p>
      <p v-if="listCheck() == -1"><button @click="sendRequest(profil.email)" class="btn btn-primary">Send Friend Request</button></p>
      <p v-else><button @click="deleteContact(listCheck())" class="btn btn-primary">Unfriend</button></p>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import UserApiService from '../../services/UserApiService';
    import ContactApiService from '../../services/ContactApiService';

  export default {
    data() {
        return {
            profil: {},
            contactList: []
        };
    },

    async mounted() {
        this.profilEmail = this.$route.params.email;
        this.profil = await UserApiService.getUserAsync(this.profilEmail);
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        await this.getList();
    },

    methods: {
        ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

        async getList() {
            this.contactList = await ContactApiService.getContactListAsync(this.user.userId);
        },

        listCheck() {
            var result = -1;
            for(var i = 0; i < this.contactList.length; i++)
            {
                if(this.contactList[i].userEmail == this.profil.email || this.contactList[i].friendEmail == this.profil.email)
                {
                    result = this.contactList[i].contactId;
                }
            }
            return result;
        },

        async deleteContact(contactId) {
            try {
                await ContactApiService.deleteContactAsync(contactId);
            } catch (error) {
                
            }
            this.$router.replace("/contacts");
        },

        async sendRequest(friendEmail)
        {
            var recipientsUser = await UserApiService.getUserAsync(friendEmail);
            
            var model = {};
            model.userId = this.user.userId;
            model.friendId = recipientsUser.userId;
            model.invitation = false;
            
            this.$router.replace("/contacts");
            await ContactApiService.createContactAsync(model);
        }
    }
    };
</script>