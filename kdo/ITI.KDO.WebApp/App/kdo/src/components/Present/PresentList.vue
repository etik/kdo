<template>
<div>
    <div class="container">
      <div class="page-header">
            <h1>Presents List</h1>
      </div>
      <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`presents/create`"><i class="glyphicon glyphicon-plus"></i>Add a present</router-link>
            </div>
      </div>
      </div>
     <b-card v-for="i of presentList"
            style="max-width: 20rem; margin-top: 100px; margin-left:10px;"
            img-src="https://placekitten.com/380/200"
            img-alt="Image"
            img-top>
        <h4 slot="header">{{i.presentName}}</h4>
        <button @click="deletePresent(i.presentId)"  class="btn btn-primary">Remove</button>
        <router-link :to="`presents/edit/${i.presentId}`">Edit Present</router-link>
    </b-card>
  <b-img src="../assets/plus.jpg"></b-img> 
</div>
</template>
<script>
  import { mapActions } from 'vuex';
   import AuthService from "../../services/AuthService";
    import PresentApiService from '../../services/PresentApiService';
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            
            presentList: [],
           
            
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
            this.presentList = await PresentApiService.getPresentListAsync(this.user.userId);
      },

      async deletePresent(presentId) {
          try {
              await PresentApiService.deletePresentAsync(presentId);
              await this.refreshList();
          }
          catch(error) {

          }
      }
  }
  };

</script>
<style lang="less">

</style>