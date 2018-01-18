<template>
<div>
    <div class="row">
        <div class="col-sm-4">
            <b-card title=" Your Profil"
                        tag="article"
                        style="max-width: 20rem;"
                        class="mb-2">
                 <img :src="'data:image/jpeg;base64,'+ item.photo" style="width:100%" class="img-thumbnail myImage"
                        img-alt="Image"
                        img-top/>
                <h4>Chose a picture</h4>
                            <div class="input-group">
                                <label class="input-group-btn">
                                    <span class="btn btn-primary btn-file">
                                        Browse
                                        <input type="file" @change="onFileChange" style="display: none;" multiple>
                                    </span>
                                </label>
                                <input type="text" class="form-control" v-model="sendImage.name" readonly>
                            </div>
                            <button v-show="item.photo != null || image != null" class="btn btn-success" @click="removeImage($event)">Delete picture</button>
                            
                            <a @click="setPhoto()">
                                <button type="button" class="btn btn-success" aria-label="Left Align">
                                    <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                                    Set your picture
                                </button>
                            </a> 
                <p class="card-text">
                    {{auth.email}}
                    </br>
                    {{item.firstName}}
                    </br>
                    {{item.lastName}}
                    </br>
                    <p class=""><b><i class="glyphicon glyphicon-earphone mysize"></i>&nbsp;Téléphone</b></p>
                    <p>{{item.phone}}</p>
            
                </p>
            </b-card>
      </div>
  <div class="col-sm-8">
   <b-card title="Edit your Profil">

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
            <b-form-group  label="Phone:">
                <b-form-input asp-for="Phone" class="form-control" v-model="item.phone">
                <span asp-validation-for="Phone"></span>
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
    import FileApiService from "../../services/FileApiService";
    import { mapGetters, mapActions } from "vuex";
    import "../../directives/requiredProviders";
    import Vue from 'vue';
    import Vuex from 'vuex';

    export default {
        data() {
            return {
                userEmail: null,
                item: {},
                image: '',
                sendImage: '',
                errors: [],
                data: new FormData(),
                TypeOfFile: 1
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

            async refresh(){
                this.activeUser = await UserApiService.getUserAsync(this.item.userEmail);
                try {
                    this.id = this.$route.query.Id;
                    this.item = await UserApiService.getUserAsync(this.id);
                }
                catch(error) {
                    this.item = this.activeUser;
                }
            },

            async onSubmit(e) {
                try{
                    await UserApiService.updateUserAsync(this.item);
                    if(item.sendImage != null) setPhoto();
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
                 //this.$router.replace('userProfil');
            },

            async setPhoto() {
                console.log("setPhoto is active");
              
                if(this.data != null)
                {
                    console.log("this.image is not null");
                    console.log("this.data : " + this.data);
                    console.log("this.item.userId : " + this.item.userId);
                    this.sendItemImage = await FileApiService.updateFileAsync(this.data, this.item.userId, this.TypeOfFile);
                }  
                
                
                // this.refresh();
                // this.$router.replace('userProfil');
            },

            onFileChange(e) {
                console.log("in OnFileChange");             
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                this.createImage(files[0]);
                this.refresh();
                this.$router.replace('userProfil');      
            },
            createImage(file) {
                console.log("in CreateImage");   
                var image = new Image();
                var reader = new FileReader();
                var vm = this;

                this.data.append('files', file);
                this.sendImage = file;
                console.log("sendImage : " + this.sendImage);
                
                reader.onload = (e) => {
                    vm.image = e.target.result;
                };
                reader.readAsDataURL(file);
            },
            async removeImage(e) {
                console.log("in RemoveImage");   
                e.preventDefault();
                this.image = '';
                this.item.photo = '';
                this.data.append('files',  this.item.photo);
                this.sendItemImage = await this.executeAsyncRequest(() => FileApiService.updateFileAsync(this.data, this.item.userId, this.TypeOfFile));
                this.mode = undefined;
                 this.refresh();
                this.$router.replace('userProfil');           
            }
        }
    };
</script>

<style lang="less" scoped>
.myImage {
    max-width: 100%;
    max-height: 25%;
}

</style>

<style lang="less">
    @import "../../styles/global.less";
</style>