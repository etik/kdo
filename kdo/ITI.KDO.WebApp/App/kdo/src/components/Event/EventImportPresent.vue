<template>
    <div class="container">
      <h2>Your Presents List</h2>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Present Id</th>
                    <th>Present Name</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="presentList.length == 0">
                    <td colspan="7" class="text-center">We-want-a-present!!!</td>
                </tr>

                <tr v-for="i of presentList">
                    <td>{{ i.presentId }}</td>
                    <td>{{ i.presentName }}</td>
                    <td>
                        <button @click="addPresent(i.presentId)"  class="btn btn-primary">Add</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <h2>Event Presents List</h2>
        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Present Id</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="presentList.length == 0">
                    <td colspan="7" class="text-center">We-want-a-present!!!</td>
                </tr>

                <tr v-for="i of quantityList">
                    <td>{{ i.presentId }}</td>
                    <td>
                        <button @click="removePresent(i.presentId)"  class="btn btn-primary">Remove</button>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import PresentApiService from '../../services/PresentApiService';
    import UserApiService from '../../services/UserApiService';
    import EventApiService from '../../services/EventApiService';
    import QuantityApiService from '../../services/QuantityApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            presentList: [],
            quantityList: [],
            itemQuantity: {}
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));

        console.log(this.eventId);
        await this.refreshPresentList();
        await this.refreshQuantityList();
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshPresentList() {
            this.presentList = await PresentApiService.getPresentListAsync(this.user.userId);
      },
      async refreshQuantityList(){
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityListAsync(this.eventId));
      },
      
      async addPresent(presentId) {
          try {/*
              itemQuantity.quantity = 1;
              itemQuantity.recipientId = this.user.userId;
              itemQuantity.nominatorId = this.user.userId;
              itemQuantity.eventId = this.eventId;
              itemQuantity.presentId = presentId;
              await QuantityApiService.createQuantityAsync(itemQuantity);*/
              await this.refreshQuantityList();
          }
          catch(error) {
          }
      },

      async removePresent(presentId) {
          try {
              //await QuantityApiService.deletePresentAsync(presentId);
              await this.refreshQuantityList();
          }
          catch(error) {
          }
      }
  }
  };
</script>

<style lang="less">

</style>