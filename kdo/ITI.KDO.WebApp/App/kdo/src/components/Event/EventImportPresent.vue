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
                    <th>Quantity Id</th>
                    <th>Present Id</th>
                    <th>Present Name</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="myQuantityList.length == 0">
                    <td colspan="7" class="text-center">We-want-a-present!!!</td>
                </tr>

                <tr v-for="i of myQuantityList">
                    <td>{{ i.quantityId }}</td>
                    <td>{{ i.presentId }}</td>
                    <td>{{ i.presentName }}</td>
                    <td>
                        <button @click="removePresent(i.quantityId)"  class="btn btn-primary">Remove</button>
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
            myQuantityList: [],
            itemQuantity: {}
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));

        await this.refreshPresentList();
        await this.refreshQuantityList();
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshPresentList() {
            this.presentList = await PresentApiService.getPresentListAsync(this.user.userId);
      },
      async refreshQuantityList(){
            this.myQuantityList = [];
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityPresentListAsync(this.user.userId, this.eventId));
            
            for (var i = 0; i < this.quantityList.length; i++)
            {
                if (this.quantityList[i].nominatorId == this.user.userId)
                    this.myQuantityList.push(this.quantityList[i]);
            }
      },
      
      async addPresent(presentId) {
          try {
              this.itemQuantity.quantity = 1;
              this.itemQuantity.recipientId = this.user.userId;
              this.itemQuantity.nominatorId = this.user.userId;
              this.itemQuantity.eventId = this.eventId;
              this.itemQuantity.presentId = presentId;
              await QuantityApiService.createQuantityAsync(this.itemQuantity);
              await this.refreshQuantityList();
          }
          catch(error) {
          }
      },

      async removePresent(quantityId) {
          try {
              await QuantityApiService.deleteQuantityAsync(quantityId);
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