<template>

 <div id="fond" style="margin-top:-24px;">
 
    <b-navbar toggleable="md" type="dark" variant="dark" fixed="top">

    <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

    <b-collapse is-nav id="nav_collapse">

    <div class="container-fluid">
          <!-- Brand and toggle get grouped for better mobile display -->
      <div class="navbar-header">

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">

          <b-navbar-brand href="/Home/">
            <img src="../img/logoKdo.png" style="width:50px"></img>
          </b-navbar-brand>

          <b-navbar-nav v-if="!auth.isConnected">
            <b-nav-item-dropdown left>
              <!-- Using button-content slot -->
              <template slot="button-content">
                Sign In
              </template>
              <b-dropdown-item @click="login('Google')" class="btn btn-lg btn-block btn-primary">Se connecter via Google</b-dropdown-item>
              <b-dropdown-item @click="login('Facebook')" class="btn btn-lg btn-block btn-primary">Se connecter via Facebook</b-dropdown-item>
              <b-dropdown-item @click="login('Base')" class="btn btn-lg btn-block btn-primary">Se connecter via KDO</b-dropdown-item>
            </b-nav-item-dropdown>

            <b-nav-item href="#" @click="register()">Sign Up</b-nav-item>
          </b-navbar-nav>

          <b-navbar-nav v-if="auth.isConnected">

            <b-nav-item  href="#"@click="logout()">Logout</b-nav-item>
          </b-navbar-nav>

        </b-navbar-nav>

      </div>
    </div>

    <div class="progress" v-show="isLoading">
      <div class="progress-bar progress-bar-striped active" role="progressbar" style="width: 100%"></div>
    </div>
    

    </b-collapse>
    </b-navbar>
    
    <header id="page-top" v-if="!auth.isConnected">
      <div class="container">
          <div class="row">
              <div class="col-lg-12">
                  <img class="img-responsive" src="http://ironsummitmedia.github.io/startbootstrap-freelancer/img/profile.png" alt="">
              </div>
          </div>
      </div>
    </header>

    <div class="content-wrapper">
      <section class="primary" id="portfolio"  v-if="!auth.isConnected">
          <div class="container">
              <div class="row">
                  <div class="col-lg-12 text-center">
                      <h2>All ABOUT US</h2>
                      <hr class="star-primary">
                  </div>
              </div>
              <div class="row">
                  <div class="col-sm-4">
                      <img src="http://lorempixel.com/360/260/nature/">    
                  </div>
                  <div class="col-sm-4">
                      <img src="http://lorempixel.com/360/260/animals/">    
                  </div>
                  <div class="col-sm-4">
                      <img src="http://lorempixel.com/360/260/abstract/">    
                  </div>
              </div>
          </div>
      </section>
      <section class="success" id="about"  v-if="!auth.isConnected">
          <div class="container">
              <div class="row">
                  <div class="col-lg-12 text-center">
                      <h2>Do you want a gift as you prefer ?</h2>
                      <hr class="star-light">
                  </div>
              </div>
              <div class="row">
                  <div class="col-lg-4 col-lg-offset-2">
                      <p>Start to add your list of gift on your account.</p>
                  </div>
                  <div class="col-lg-4">
                      <p>Create an event where you add your friends and your gift from your list.</p>
                  </div>
                  <div class="col-lg-4">
                      <p>Your friends will dicuss and decide for buying your gift.</p>
                  </div>
              </div>
          </div>
      </section>
    </div>
    <div>
      <b-row class="bg-light" style="height: 100%; margin-top: 15px; margin-bottom: 0px;">
          <b-col md="2" class="bg-light " style="height:100%;">
            <b-nav v-if="auth.isConnected" vertical class="icon-bar" >
              <b-nav-item href="/Home/userProfile" class="row">Profil</b-nav-item>
              <b-nav-item href="/Home/events"class="row">Event</b-nav-item>
              <b-nav-item href="/Home/contact" class="row" >Mes contacts</b-nav-item>
              <b-nav-item href="/Home/events/calendar" class="row">Calendrier</b-nav-item>
              <b-nav-item href="/Home/presents" class="row">Ma liste de cadeaux</b-nav-item>
            </b-nav>
          </b-col>

          <b-col md="8" style="height:100%;">
            <b-row>
              <router-view class="child"></router-view>
            </b-row>
          </b-col>
          <b-col md="2" class="bg-light" style="height:100%;">
          </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import $ from 'jquery'
import UserApiService from "../services/AuthService";
import { mapGetters, mapActions } from "vuex";
import "../directives/requiredProviders";
import '../directives/bsDropdown';
//import Vue from 'vue';


export default {
  data() {
    return {
      userEmail: null
    };
  },

  mounted() {
    AuthService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  beforeDestroy() {
    AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  computed: {
    ...mapGetters(["isLoading"]),
    auth: () => AuthService
  },

  methods: {
    login(provider) {
      AuthService.login(provider);
    },

    register() {
      AuthService.register();
    },

    logout(){
      this.$router.replace('/logout');
    },

    onAuthenticated() {
      this.$router.replace("/");
    }
  }
};
</script>

<style>
.row{
  margin-left:1%;
  margin-Right:0!important;
}

.active {
    background-color: #4CAF50 !important;
}
.icon-bar {
    height: 100%;
    width: max-content;
    background-color: #343a40; /* Dark-grey background */
    position: fixed;

}

.icon-bar a {
    display: block; /* Make the links appear below each other instead of side-by-side */
    text-align: center; /* Center-align text */
    transition: all 0.3s ease; /* Add transition for hover effects */
    color: white; /* White text color */
    font-size: 18px; /* Increased font-size */
    font-weight:600px;
    line-height: 30px;
    padding-top: 40%;

}

header {
  background-image: url(../assets/noel.jpg);
  width: 100%;
  background-size: 100%;
  height:400px;
  color: #fff;
  text-align: center;
  text-shadow: 0 1px 3px rgba(0, 0, 0, .5);
}

.header {
  width: 100%;
  height: 100%;
  min-height: 100%;
  -webkit-box-shadow: inset 0 0 100px rgba(0, 0, 0, .5);
  box-shadow: inset 0 0 100px rgba(0, 0, 0, .5);
}
.icon-bar a:hover {
color: #bcbcbc;
}

.active {
    background-color: #4CAF50; /* Add an active/current color */
}
</style>
<style lang="less">
  @import "../styles/global.less";
  @media screen and (max-height: 450px) {
    .icon-bar {padding-top: 15px;}
    .icon-bar a {font-size: 18px;}
}
.footer {
   position:sticky;
   left: 0;
   bottom: 0;
   width: 100%;
   background-color: #343a40;
   color: white;
   text-align: center;
}
</style>
</style>