<template>
    <div class="container">
      <div class="page-header">
            <h1>Events</h1>
      </div>
      <div class="row">
      <div class="col-sm-6">
      <b-card-group>
      <b-img rounded blank width="120" height="150" blank-color="#777" alt="img" class="m-1" />
      <div class="col-sm-6">
        <b-list-group>
        <b-list-group-item>
            {{event.eventName}}
        </b-list-group-item>
        <b-list-group-item>
            Date
        </b-list-group-item>
        </b-list-group>
      
        <b-button :to="`/events/edit/${eventId}`" variant="primary"> Edit </b-button>
        </div>
     </b-card-group>
        </div>
        </div>

      <!--div class="com-sm-6 carou">
      <b-card>
      <b-carousel width="500" height="500" id="carousel1"
                style="text-shadow: 1px 1px 2px #333;"
                controls
                indicators
                background="#ababab"
                :interval="4000"
                v-model="slide"
                @sliding-start="onSlideStart"
                @sliding-end="onSlideEnd"-->
        <!--b-col v-for="i of PresentList" md="4">
        <b-card :title="i.PresentName"
                img-src="https://img4.hostingpics.net/pics/518638Image1.png"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 16rem;"
                class="mb-2">
        <p class="card-text">
            Some quick example text to build on the card title and make up the bulk of the card's content.
        </p>
        <b-button href="#" variant="primary">Go</b-button>
        </b-card-->
        <!--/b-col>
        <b-carousel-slide  caption="First slide"
                        text="Nulla vitae elit libero, a pharetra augue mollis interdum."
                        img-src="https://lorempixel.com/1024/480/technics/2/">
        </b-carousel-slide>
        <b-carousel-slide img-src="https://lorempixel.com/1024/480/technics/4/">
        <h1>Hello world!</h1>
        </b-carousel-slide>
        </b-carousel>
            <p class="mt-4">
      Slide #: {{ slide }}<br>
      Sliding: {{ sliding }}
    </p>
    </b-card>
    <div class="com-sm-6">
      <b-card>
    </b-card>
    </div>
        </div-->

      <!--table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Participant</th>
                    <th>userId</th>
                    <th>eventId</th>
                </tr>
            </thead>

            <tbody>

                <tr v-if="participantList.length == 0">
                    <td colspan="7" class="text-center">Participant</td>
                </tr>

                <tr v-for="i of participantList">
                    <td>{{ i.ParticipantType }}</td>
                    <td>{{ i.userId }}</td>
                    <td>{{ i.eventId }}</td>
                </tr>
            </tbody>
        </table-->
        <div class="carou">
        <b-card  bg-variant="dark" text-variant="white" title="Cadeaux">
        <b-carousel id="carousel1 "
                style="text-shadow: 1px 1px 2px #333;"
                controls
                indicators
                background="#ababab"
                :interval="4000"
                img-width="1024"
                img-height="480"
                v-model="slide"
                @sliding-start="onSlideStart"
                @sliding-end="onSlideEnd">
        <b-carousel-slide caption="First slide"
                        text="Nulla vitae elit libero, a pharetra augue mollis interdum."
                        img-src="https://lorempixel.com/1024/480/technics/2/">
        </b-carousel-slide>

        <b-carousel-slide img-src="https://lorempixel.com/1024/480/technics/4/">
        <h1>Hello world!</h1>
        </b-carousel-slide>
        </b-carousel>
        </b-card>
        </div>
        <p>
        <div class="participant">
        <b-card bg-variant="dark" text-variant="white" title="Participant" >
        <div v-for="i of participantList">
        <b-button>{{i.userId}}</b-button>
        </div>
        </b-card>
        </div>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import AuthService from "../../services/AuthService";
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            participantList: [],
            slide: 0,
            sliding: null
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));

        
        this.user = await UserApiService.getUserAsync(userEmail);

        console.log(this.user.userId);
        console.log(this.eventId);
        
        await this.refreshList();
        await this.refreshParticipantList();
        console.log(this.participantList);
    },

    methods: {
    onSlideStart (slide) {
      this.sliding = true
    },
    onSlideEnd (slide) {
      this.sliding = false
    },
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.event = await EventApiService.getEventAsync(this.eventId);
      },
       async refreshParticipantList(){
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
      },
    }
  };
</script>

<style lang="less">
.row {
    margin-top:10%;
}
.carou {
    width:40%;
}

</style>