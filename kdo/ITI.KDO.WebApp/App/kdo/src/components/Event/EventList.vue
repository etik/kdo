<template>
<div>
<section>
  <div class="title">
    <h1>EVENT LIST</h1>
  </div>
</section>
    <div class="row" style="margin-left:11%;">
        <div md="12" class="feature-box event" v-for="i of eventList">
                <div class="test-event">
                <span>{{ i.eventName }}</span>
                </div>
         <div v-if = "isCreator(i.userId) == true">
            <b-img src="https://image.freepik.com/icones-gratuites/corbeille_318-55452.jpg"  class="delete"@click="deleteEvent(i.eventId)" fluid alt="Responsive image" />
            <div class="edit">
            <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg" :to="`events/edit/${i.eventId}`"></router-link>
            </div>
         </div>
         <div v-if = "isCreator(i.userId) == false">
             <router-link tag="img" class="calendar" src="https://png.icons8.com/metro/540/calendar.png" :to="`events/dateSuggest/${i.eventId}`"></router-link>
             <router-link tag="img" class="voir" src="http://www.icone-png.com/png/24/23722.png" :to="`events/view/${i.eventId}`"></router-link>
            <b-img src="http://cdn.onlinewebfonts.com/svg/img_447829.png"  class="quit" @click="quitEvent(i.eventId) " fluid alt="Responsive image" />

         </div>
                
        </div>  
        <div md="12" class="feature-box1 test">
                    <router-link tag="img" style=" margin-top: -5%;" src="https://blazer-net.com/wp-content/uploads/blazer-nett.png" :to="`events/create`">Add a present</router-link>
                       <span style="font-family: cursive; font-size: larger;" >Add an event</span>

        </div>
	</div>
    </div> <!-- End Col -->


      <!--div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`events/create`"><i class="glyphicon glyphicon-plus"></i>Add an event</router-link>
            </div>
      </div>

      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Event Name</th>
                    <th>Description</th>
                    <th>Date</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="eventList.length == 0">
                    <td colspan="7" class="text-center">Event</td>
                </tr>

                <tr v-for="i of eventList">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.descriptions }}</td>
                    <td>{{ i.dates }}</td>
                    <td>
                    <b-button-group v-if = "isCreator(i.userId) == true">
                        <b-button :to="`events/edit/${i.eventId}`">Edit</b-button>
                        <b-button :to="`events/view/${i.eventId}`">View</b-button>
                        <b-button @click="deleteEvent(i.eventId) ">Remove</b-button>
                    </b-button-group>

                    <b-button-group v-if = "isCreator(i.userId) == false">
                        <b-button :to="`events/edit/${i.eventId}`">Suggest another date</b-button>
                        <b-button :to="`events/dateSuggest/${i.eventId}`">View</b-button>
                        <b-button @click="quitEvent(i.eventId) ">Quit event</b-button>
                    </b-button-group>

                    <td-->
                        <!--button @click="deleteEvent(i.eventId)"  class="btn btn-primary">Remove</button>
                        <router-link :to="`events/edit/${i.eventId}`">Edit event</router-link>
                        <router-link :to="`events/view/${i.eventId}`">View</router-link-->

                    <!--/td>
                </tr>

            </tbody>
        </table>
    </div>
    </div-->
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import ParticipantApiService from '../../services/ParticipantApiService';
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            eventList: []
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
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
        },
        async deleteEvent(eventId) {
            try {
                await EventApiService.deleteEventAsync(eventId);
                await this.refreshList();
            }
            catch(error) {

            }
        },

        isCreator(creatorId){
            if(this.user.userId == creatorId){
                return true;
            }else {
                return false;
            }
        },

        async quitEvent(eventId){
            await ParticipantApiService.deleteParticipantAsync(this.user.userId, eventId);
            await this.refreshList();
        }
  }
  };
</script>

<style lang="less">
.row{
    margin-top: 10px;
}

/* Section - Title */
/**************************/
.title {background: white; padding: 60px; margin:0 auto; text-align:center;}
.title h1 {font-size:35px; letter-spacing:8px;}

.test div span{
    color: white;
}
.quit {
    width: 7%;
    margin-left: 99%;
    margin-top: -137%;
}

.calendar {
    width: 7%;
    margin-left: 73%;
    margin-top: -110%;
}
.voir{
     width: 7%;
    margin-left: 89%;
    margin-top: -123%;
}
.test-{
    &event {
        position: relative;
        right:0;
        font-size: 24px;
        background-color: #d44e4e;
        opacity: 0.8;
        margin-top:32%;
        &:hover{
            -webkit-animation: dude .75s ;
            -moz-animation: dude .75s;
            -o-animation: dude .75s;
            animation: dude .75s;
        }
    }
}

.feature-box{
   // background-image:url("https://financesonline.com/uploads/2017/10/ev.jpg");
    width: 400px;
    height: 200px;
    margin-top: 2%;
    margin-right: 5%;
}
</style>