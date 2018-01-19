<template>
<div>
<section>
  <div class="title">
    <h1>PRESENTS LIST</h1>
  </div>
</section>
    <div class="row" style="margin-left:11%;">
        <div md="12" class="feature-box test" v-for="i of presentList">
                <div class="test-event">
                <span>{{ i.presentName }}</span>
                </div>
            <b-img src="https://image.freepik.com/icones-gratuites/corbeille_318-55452.jpg"  class="delete"@click="deletePresent(i.presentId)" fluid alt="Responsive image" />
                <div class="edit">  
            <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg" :to="`presents/edit/${i.presentId}`">Edit Present</router-link>
                </div>
        </div>  
        <div md="12" class="feature-box1 test">
                    <router-link tag="img" style=" margin-top: -5%;" src="https://blazer-net.com/wp-content/uploads/blazer-nett.png" :to="`presents/create`">Add a present</router-link>
                       <span style="font-family: cursive; font-size: larger;" >Add a present</span>

        </div>
	</div> <!-- End Col -->
						
    <!--b-img v-for=" i of presentList" src="https://placekitten.com/380/200">
        <h1>{{i.presentName}}</h1>
    </b-img>

     <b-card md="12" v-for=" i of presentList"
            style="max-width: 20rem; margin-top: 100px; margin-left:10px;"
            img-src="https://placekitten.com/380/200"
            img-alt="Image"
            img-top>
        <h4 slot="header">{{i.presentName}}</h4>
        <b-img src="https://image.freepik.com/icones-gratuites/corbeille_318-55452.jpg" @click="deletePresent(i.presentId)" style="width: 14%;" fluid alt="Responsive image" />
          <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg" style="width: 14%;" :to="`presents/edit/${i.presentId}`">Edit Present</router-link>
    </b-card>
    <router-link tag="img" src="http://icons.iconarchive.com/icons/icons8/ios7/512/User-Interface-Plus-icon.png"  style="width: 5%; height:5%;" :to="`presents/create`"></router-link>
                 <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`presents/create`"><i class="glyphicon glyphicon-plus"></i>Add a present</router-link>
            </div>
      </div-->
</div>
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

/* Section - Title */
/**************************/
.title {background: white; padding: 60px; margin:0 auto; text-align:center;}
.title h1 {font-size:35px; letter-spacing:8px;}

.delete {
    width: 7%;
    margin-left: 77%;
    margin-top: -111%;
}
.edit {
    width: 7%;
    margin-left: 91%;
    margin-top: -62%;
}
.feature-box{
   // background-image:url("http://www.neorizons-travel.com/wp-content/uploads/2012/02/pho_neorizons.jpg");
    width: 400px;
    height: 200px;
    margin-top: 2%;
    margin-right: 5%;
}

.feature-box1{
    border-style: ridge;
    width: 400px;
    height: 200px;
    margin-top: 2%;
    margin-right: 5%;
}

.feature-box:hover{
    box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
    transform: scale(1.3);
}

.test div span{
    color: white;
}

.test-{
    &event {
        position: relative;
        right:0;
        font-size: 24px;
        background-color: #1a5d59;
        opacity: 0.8;
        margin-top:32%;
    }
}

@-webkit-keyframes dude {
    0% {
        width: 0;
    }
    100% {
        width: 100%;
    }
}
@-moz-keyframes dude {
    0% {
        width: 0;
    }
    100% {
        width: 100%;
    }
}
@-o-keyframes dude {
    0% {
        width: 0;
    }
    100% {
        width: 100%;
    }
}
@keyframes dude {
    0% {
        width: 0;
    }
    100% {
        width: 100%;
    }
}
</style>