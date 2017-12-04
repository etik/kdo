<template>
    <div>
        <div class="text-center" style="padding: 50px">
            <button type="button" @click="sendEmail('OccasionInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Send Occasion Invitation</button>
            <button type="button" @click="sendEmail('FriendInvitation')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-facebook-square" aria-hidden="true"></i> Send Friends Invitation</button>
        </div>
    </div>
</template>

<script>
import AuthService from "../../services/AuthService";
import Vue from "vue";
import $ from "jquery";
import Vuex from 'vuex';

export default {
  data() {
    return {
      endpoint: null
    };
  },

  methods: {
      sendEmail(mailType){
          AuthService.sendEmail(mailType);
      }
  }
};
</script>