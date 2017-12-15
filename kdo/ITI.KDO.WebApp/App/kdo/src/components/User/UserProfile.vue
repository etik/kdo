<template>
<div>
<div class="row">
  <div class="col-sm-4">
    <b-card title=" Your Profile"
       img-src="https://www.walldevil.com/wallpapers/a18/thumb/background-lemons-web-nvstormygetaway-images-profile-naver.jpg"
            img-alt="Image"
            img-top
            tag="article"
            style="max-width: 20rem;"
            class="mb-2">
        <p class="card-text">
            {{auth.email}}
            </br>
            {{item.firstName}}
            </br>
            {{item.lastName}}
            
        </p>
    </b-card>
      </div>
  <div class="col-sm-8">
   <b-card title="Edit your Profile">
<!--form class="form-inline" asp-controller="Account" asp-action="Register" method="post" @submit="onSubmit($event)">
            <div class="col-md-12">
            <div class="form-group">
                <label  asp-for="Email">Email : </label>
                <input asp-for="Email" class="form-control" v-model="auth.email"/>
                <span asp-validation-for="Email"></span>
            </div>
            </div>
            <div class="col-md-6">
            <div class="form-group">
                <label asp-for="FirstName">FirstName : </label>
                <input asp-for="FirstName" class="form-control" v-model="item.firstName"/>
                <span asp-validation-for="FirstName"></span>
            </div>
            </div>
            <div class="col-md-6">
            <div class="form-group">
                <label asp-for="LastName">LastName : </label>
                <input asp-for="LastName" class="form-control" v-model="item.lastName"/>
                <span asp-validation-for="LastName"></span>
            </div>
            </div>
            <div class="col-md-6">
            <div class="form-group">
                <label asp-for="Birthdate">BirthDate : </label>
                <input asp-for="Birthdate" class="form-control" type="date" v-model="item.birthdate"/>
                <span asp-validation-for="Birthdate"></span>
            </div>
            </div>
            <div class="col-md-6">
            <div class="form-group">
                <label asp-for="Phone">Phone : </label>
                <input asp-for="Phone" class="form-control" v-model="item.phone"/>
                <span asp-validation-for="Phone"></span>
            </div>
            </div-->
            <!--div class="col-md-2">
            <div class="form-group">
                <label asp-for="Photo">Photo : </label>
                <input asp-for="Photo" class="form-control" v-model="item.photo"/> <br />
                <img :src="item.photo" />
                <span asp-validation-for="Photo"></span>
            </div>
            </div-->
            <!--input type="submit" class="btn btn-primary btn-block" value="Edit"/>

        </form--> 
  <b-form  asp-controller="Account" asp-action="Register" method="post" @submit="onSubmit($event)" >
            <b-col md="12">
            <b-form-group label="Email:">
                <b-form-input asp-for="Email" class="form-control" v-model="auth.email">
                <span asp-validation-for="Email"></span>
                </b-form-input>
            </b-form-group>
            </b-col>

            <b-col md="6">
            <b-form-group  label="Firstname:">
                <b-form-input  asp-for="FirstName" class="form-control" v-model="item.firstName">
                <span asp-validation-for="FirstName"></span>
                </b-form-input>
            </b-form-group>
            </b-col>

            <b-col md="6">
            <b-form-group  label="Lastname:">
                <b-form-input asp-for="LastName" class="form-control" v-model="item.lastName">
                <span asp-validation-for="LastName"></span>
                </b-form-input>
            </b-form-group>
            </b-col>

            <b-col md="6">
            <b-form-group  label="BirthDate:">
                <b-form-input asp-for="Birthdate" class="form-control" type="date" v-model="item.birthdate">
                <span asp-validation-for="Birthdate"></span>
                </b-form-input>
            </b-form-group>
            </b-col>

            <b-col md="6">
            <b-form-group  label="Photo:">
                <b-form-input asp-for="Photo" class="form-control" type="photo" v-model="item.photo">
                <span asp-validation-for="Photo"></span>
                </b-form-input>
            </b-form-group>
            </b-col>
            
            <b-col md="6">
            <b-button type="submit" variant="primary">Submit</b-button>
            <b-button @click="modifyPassword()" variant="primary">Modify password</b-button>
            </b-col>
        </b-form>
   </b-card>

  </div>
</div>

</div>
</template>
<script>
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import { mapGetters, mapActions } from "vuex";
    import "../../directives/requiredProviders";
    import Vue from 'vue';
    import Vuex from 'vuex';

    export default {
        data() {
            return {
                userEmail: null,
                item: {},
                errors: []
            };
        },

        computed: {
            ...mapGetters(["isLoading"]),
            auth: () => AuthService
        },

        async mounted() {
            var userEmail = AuthService.emailUser();
            this.item = await UserApiService.getUserAsync(userEmail);
        },

        methods: {
           ...mapActions(['notifyLoading', 'notifyError']),

            modifyPassword(){
                AuthService.modifyPassword();
            },
            async onSubmit(e) {
                try{
                    await UserApiService.updateUserAsync(this.item);
                }
                catch (error){
                    this.notifyError(error);
                    
                    // Custom error management here.
                    // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                    // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                    // By the way, you can handle errors manually for each component if you need it...
                }
                finally {
                    this.notifyLoading(false);
                 }
                 this.$router.replace('userProfile');
            },
        }
    };
</script>

<style lang="less" scoped>
.row {
    margin-top:0%;
}
.progress {
    margin: 0px;
    padding: 0px;
    height: 5px;
}

a.router-link-active {
    font-weight: bold;
}
.card{
    color:#343a40;
    margin-right: -15px;
    margin-left: -15px;
    padding:6px;

}
.card-text {
    position:center;
}

</style>

<style lang="less">
    @import "../../styles/global.less";
</style>