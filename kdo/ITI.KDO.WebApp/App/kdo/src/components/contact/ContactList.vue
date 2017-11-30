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
                    <td><img :src="item.photo" /></td>
                    <td>{{ i.firstName }}</td>
                    <td>{{ i.lastName }}</td>
                    <td>
                        <button @click="deleteContact(i.contactId)"  class="btn btn-primary">Remove</button>
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

      async deleteContact(contactId) {
          try {
              await ContactApiService.deleteContactAsync(contactId);
              await this.refreshList();
          }
          catch(error) {

          }
      },

      async onSubmit(e){
            await this.executeAsyncRequest(() => ContactApiService.createContactAsync(this.contact));
      }
  }
  };
</script>

<style lang="less">

</style>