<template>
  <div>
        <b-carousel id="friend_carousel"
                style="text-shadow: 1px 1px 2px #333; margin-top: 20px; margin-bottom: 20px;"
                controls
                indicators
                :interval="0"
                img-width="1024"
                img-height="412"
                v-model="slide"
                @sliding-start="onSlideStart"
                @sliding-end="onSlideEnd">

          <b-carousel-slide v-for="i in nbslide" img-blank variant="dark" img-alt="Blank image">
            <b-row>
              <b-col md="4">
                <b-card v-if="eventList[(3 * (i - 1))] != null"
                      img-src="https://img4.hostingpics.net/pics/518638Image1.png"
                      img-alt="Image"
                      img-top
                      tag="article"
                      style="max-width: 16rem;"
                      class="mb-2">
                <h2 class="card-text">
                  {{eventList[(3 * (i - 1))].eventName}}
                </h2>
                <router-link :to="`events/view/${eventList[(3 * (i - 1))].eventId}`">Go</router-link>
                </b-card>
              </b-col>

              <b-col md="4">
                <b-card v-if="eventList[(3 * (i - 1)) + 1] != null"
                      img-src="https://img4.hostingpics.net/pics/211052Image2.png"
                      img-alt="Image"
                      img-top
                      tag="article"
                      style="max-width: 16rem;"
                      class="mb-2">
                <h2 class="card-text">
                  {{eventList[(3 * (i - 1)) + 1].eventName}}
                </h2>
                <router-link :to="`events/view/${eventList[(3 * (i - 1)) + 1].eventId}`">Go</router-link>
                </b-card>
              </b-col>

              <b-col md="4">
                <b-card v-if="eventList[(3 * (i - 1)) + 2] != null"
                      img-src="https://img4.hostingpics.net/pics/527500Image3.png"
                      img-alt="Image"
                      img-top
                      tag="article"
                      style="max-width: 16rem;"
                      class="mb-2">
                  <h2 class="card-text">
                    {{eventList[(3 * (i - 1)) + 2].eventName}}
                  </h2>
                  <router-link :to="`events/view/${eventList[(3 * (i - 1)) + 2].eventId}`">Go</router-link>
                </b-card>
              </b-col>
            </b-row>
        </b-carousel-slide>
    </b-carousel>
  </div>
    
  <!--div class="row">
    <b-col class="com-sm-5" v-for="i of eventList">
      <b-card title="Your Event">
        <p class="card-text">
          {{i.eventName}}
        </p>
      </b-card>
    </b-col>
  </div-->
</template>

<script>
import { mapActions } from 'vuex';
import AuthService from "../services/AuthService";
import EventApiService from '../services/EventApiService';
import UserApiService from '../services/UserApiService';

export default {
    data () {
        return {
            slide: 0,
            sliding: null,

            userEmail: null,
            user: {},
            eventList: [],
            nbslide: 0
        };
    },
     async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
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
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            this.nbslide = Math.trunc((this.eventList.length + 2) / 3);
      },
      async deleteEvent(eventId) {
          try {
              await EventApiService.deleteEventAsync(eventId);
              await this.refreshList();
          }
          catch(error) {
          }
      }
  }
  };
</script>

<style>
.img-fluid {
    background-color: grey;
}
</style>