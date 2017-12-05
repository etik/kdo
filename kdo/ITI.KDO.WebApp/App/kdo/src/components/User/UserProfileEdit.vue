<template>
    <div class="container">
        <div class="page-header">
            <h1 style="margin: 20px;">Edit your profile</h1>
        </div>

        <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>
                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
        </div>
        
        </br>
        <div class="form-group" >
                <label asp-for="Photo">Photo : </label>
                <div class="thumbnail">
                     <img src="../../assets/user.jpg" style="width:30%" class="img-thumbnail"></img>
                </div>
        </div>

        <div class="col-md-4 pickPhoto">
                    <label>Sélectionner une image : </label>
                    <div class="input-group">
                    <label class="input-group-btn">
                    <span class="btn btn-primary btn-file">
                        Browse
                        <input type="file" @change="onFileChange" style="display: none;" multiple>
                    </span>
                    </label>
                    </div>
               
                <input type="text" class="form-control">
        </div>
        
        </br></br></br></br></br>
        <form asp-controller="Account" asp-action="Register" method="post" @submit="onSubmit($event)">
            <div class="form-group">
                <label asp-for="Email">Email : </label>
                <input asp-for="Email" class="form-control" v-model="auth.email"/>
                <span asp-validation-for="Email"></span>
            </div>
            <div class="form-group">
                <label asp-for="FirstName">FirstName : </label>
                <input asp-for="FirstName" class="form-control" v-model="item.firstName"/>
                <span asp-validation-for="FirstName"></span>
            </div>
            <div class="form-group">
                <label asp-for="LastName">LastName : </label>
                <input asp-for="LastName" class="form-control" v-model="item.lastName"/>
                <span asp-validation-for="LastName"></span>
            </div>

            <div class="form-group">
                <label asp-for="Birthdate">BirthDate : </label>
                <input asp-for="Birthdate" class="form-control" type="date" v-model="item.birthdate"/>
                <span asp-validation-for="Birthdate"></span>
            </div>
            <div class="form-group">
                <label asp-for="Phone">Phone : </label>
                <input asp-for="Phone" class="form-control" v-model="item.phone"/>
                <span asp-validation-for="Phone"></span>
            </div>
                      
            <input type="submit" class="btn btn-primary btn-block" value="Edit"/>

        </form>
    </div>
</template>
<script>
import { mapGetters, mapActions } from "vuex";
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import "../../directives/requiredProviders";
    import Vue from 'vue';
    import Vuex from 'vuex';

    export default {
        data() {
            return {
                userEmail: null,
                item: {},
                img: null,
                errors: [],
                success: [],
                image: '',
                sendImage: '',
                data: new FormData()
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

            async onSubmit(e) {
                    try{
                        this.setPhoto();
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
                
                this.$router.replace('edit');
            },

            async setPhoto() {
                this.success = [];
                try
                {
                    if(this.image != null)  this.sendItemImage = await this.executeAsyncRequest(() => UserApiService.updateFileAnsync(this.data, this.item.userId));
                }
                catch(error)
                {
                    this.errors.push({responseText:"La photo n'est pas valide, veuillez essayer avec une autre image ou de la redimensionner."});
                }
                
                if (this.errors.length == 0)
                {
                    this.success.push({responseText:"La photo a bien été modifiée."});
                }
                 this.refresh();
                 
            },

            onFileChange(e) {             
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                this.createImage(files[0]);    
            },
            createImage(file) {
                var image = new Image();
                var reader = new FileReader();
                var vm = this;

                this.data.append('files', file);
                this.sendImage = file;
                console.log(this.sendImage);
                
                reader.onload = (e) => {
                    vm.image = e.target.result;
                };
                reader.readAsDataURL(file);
            },
        }
    }
</script>

<style lang="less" scoped>
    .progress {
    margin: 0px;
    padding: 0px;
    height: 5px;
    }

    a.router-link-active {
    font-weight: bold;
    }
</style>

<style lang="less">
    @import "../../styles/global.less";

    .pickPhoto{
        margin-top : -18%;
        margin-left: 35%;
    }
</style>