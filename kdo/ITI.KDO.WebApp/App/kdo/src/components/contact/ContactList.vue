<template>
    <div class="container">
      <div class="page-header">
            <h1>Contact List</h1>
      </div>

      <div class="panel panel-default">
            <div class="panel-body text-right">
                <form @submit="onSubmit($event)">
                    <input type="text" class="form-control" placeholder="Email" v-model="contact" required>
                    <button type="submit" class="btn btn-primary">Add a contact</button>
                </form>
            </div>
      </div>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Image</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="contactList.length == 0">
                    <td colspan="7" class="text-center">Friendly friends</td>
                </tr>

                <tr v-for="i of contactList">
                    <td v-if="contactInfo[contactList.indexOf(i)] = getContactInfo(i.friendId)">
                        <img :src="contactInfo[contactList.indexOf(i)].photo" />
                    </td>
                    <td>{{ contactInfo[contactList.indexOf(i)].firstName }}</td>
                    <td>{{ contactInfo[contactList.indexOf(i)].lastName }}</td>
                    <td>
                        <button @click="deleteContact(i.userId, i.friendId)"  class="btn btn-primary">Remove</button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import ContactApiService from '../../services/ContactApiService';
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            contactList: [],
            contact: null,
            contactInfo: [],
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
            this.contactList = await ContactApiService.getContactListAsync(this.user.userId);
      },

      async deleteContact(userId, friendId) {
          try {
              await ContactApiService.deleteContactAsync(userId, friendId);
              await this.refreshList();
          }
          catch(error) {

          }
      },

      async getContactInfo(friendId) {
          return await UserApiService.getUserById(friendId);
      },

      async onSubmit(e){
            var contact = await UserApiService.getUserAsync(this.contact);
            var model = {};
            model.UserId = this.user.userId;
            model.FriendId = contact.userId;
            await this.executeAsyncRequest(() => ContactApiService.createContactAsync(model));
      }
  }
  };
</script>

<style lang="less">

</style>