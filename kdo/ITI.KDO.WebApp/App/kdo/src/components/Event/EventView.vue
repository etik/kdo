<template>
<div>
<section>
  <div class="title">
    <h1>YOUR EVENT</h1>
  </div>
</section>
<b-row>
    <b-col md="4">
        <b-card style="margin-left: 10%;"   class="text-center"header-bg-variant="danger" bg-variant="light" header="Informations">
            <h6 slot="header"class="mb-0" text-variant="white">INFORMATIONS</h6>
            <b-col sm="3" >
                <b-img rounded blank width="90" height="90" blank-color="#777" alt="img" class="m-1" />
            </b-col>
            <b-col sm="12">
                <router-link tag="img" class="edite" src="https://image.flaticon.com/icons/svg/84/84380.svg" :to="`/events/edit/${eventId}`"></router-link>
                <div class="Info">{{event.eventName}}
                    <br>Date
                </div>
            </b-col>
        </b-card>
        <br>
        <b-card style="margin-left:10%;" class="text-center" bg-variant="light" header="PARTICIPANTS">
            <h6 slot="header"class="mb-0">PARTICIPANTS</h6>
            <b-card v-for="i in participantUserList"
                tag="article"
                style="max-width: 16rem;margin-left:23%;"
                class="mb-2">
                <router-link :to="`/userProfile/display/${i.email}`">{{i.firstName}} {{i.lastName}}</router-link>
            </b-card>
        </b-card>
    </b-col>
    <b-col md="8">
        <b-row>
            <b-col sm="2">
                Present for :
            </b-col>
            <b-col sm="10">
                <b-form-select v-model="selected" :options="beneficiary" v-on:change="refreshQuantityList()" class="mb-3">
                </b-form-select>
            </b-col>
        </b-row>

        <b-card bg-variant="light" header="PRESENT">
            <h6 slot="header" class="mb-0 text-center" color="white">
                PRESENTS
            </h6>

            <b-dropdown id="ddown1" text="Add a present" class="m-md-2" >
                <b-dropdown-item :to="`/events/presents/create/${eventId}`">Create a new present</b-dropdown-item>
                <b-dropdown-item :to="`/events/importPresent/${eventId}`">Import from your list of present</b-dropdown-item>
            </b-dropdown>

            <div class="row" style="margin-left:11%;">
                <div md="12" class="feature-box event"  v-for="i in quantityPresentList">
                    <div :id="'Popover-'+i.quantityId" class="test-event event-text">
                        {{ i.presentName }}
                        <b-progress :value="i.ammount" :max="i.price" show-progress animated></b-progress>
                        <b-popover :target="'Popover-'+i.quantityId" triggers="hover focus" placement="top">
                            <template slot="title">Participant</template>
                            <div v-for="x in i.participants">
                                {{x.firstName}} {{x.lastName}} 
                            </div>
                        </b-popover>
                    </div>

                    <div class="btni">
                        <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg"  class="editP" :to="`/events/presents/edit/${eventId}/${i.quantityId}`" v-if="i.nominatorId == user.userId"></router-link>
                        <router-link tag="img" src="https://cdn2.iconfinder.com/data/icons/sales-and-delivery/128/easy-2-512.png"  class="participant" :to="`/events/participate/${eventId}/${i.quantityId}`"></router-link>
                    </div>
                </div>
            </div>
        </b-card>
    </b-col>
     </b-row>
     {{bugfix}}
  </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import ParticipationApiService from '../../services/ParticipationApiService';
    import AuthService from "../../services/AuthService";
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';
    import QuantityApiService from '../../services/QuantityApiService';
    import PresentApiService from '../../services/PresentApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            participantList: [],
            beneficiary: [],
            participantUserList: [],
            quantityList: [],
            selected: null,
            quantityPresentList: [],
            bugfix: 1
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventByIdAsync(this.eventId));
        this.user = await UserApiService.getUserAsync(userEmail);
        await this.refreshParticipantList();
        this.selected = this.beneficiary[0].value;
        await this.refreshQuantityList();                    
    },

    methods: {
        ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

        async refreshParticipantList(){
            this.participantUserList = [];
            this.beneficiary = [];
            var aux;
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);

            for(var i = 0; i < this.participantList.length; i++)
            {
                aux = await UserApiService.getUserByIdAsync(this.participantList[i].userId);
                this.participantUserList.push(aux);
                if (this.participantList[i].participantType == true)                    
                    this.beneficiary.push({value: aux.userId, text: aux.firstName});
            }
        },
        async refreshQuantityList(){
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityListAsync(this.eventId));
            this.quantityPresentList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityPresentListAsync(this.selected, this.eventId));
            if (this.selected == this.user.userId)
            {
                for (var i = 0; i < this.quantityPresentList.length; i++)
                {
                    if (this.quantityPresentList[i].nominatorId != this.user.userId)
                    {
                        this.quantityPresentList.splice(i, 1);
                        i--;
                    }
                }
            }
            await this.refreshParticipation();
            await this.refreshParticipantToQuantityList();
        },
        async refreshParticipation(){
            var ammount;
            for (var i = 0; i < this.quantityPresentList.length; i++)
            {
                ammount = 0;
                await this.executeAsyncRequest(
                    () => ParticipationApiService
                        .getParticipationByQuantityAsync(this.quantityPresentList[i].quantityId)
                ).then(p => {
                    this.quantityPresentList[i].participants = p;
                    this.quantityPresentList[i].ammount = 0;
                    for (var j = 0; j < p.length; j++)
                    {
                        this.quantityPresentList[i].ammount += p[j].amountUserPrice;
                    }
                });
            }
        },
        async refreshParticipantToQuantityList(){
            for (var i = 0; i < this.quantityPresentList.length; i++)
            {
                for (var j = 0; j < this.quantityPresentList[i].participants.length; j++)
                {
                    var auxUser = await UserApiService.getUserByIdAsync(this.quantityPresentList[i].participants[j].userId);
                    this.quantityPresentList[i].participants[j].firstName = auxUser.firstName;
                    this.quantityPresentList[i].participants[j].lastName = auxUser.lastName;
                }
            }
            this.bugfix++;
        }
    }
};
</script>

<style lang="less">
.row {
    margin-top:10%;
}
.event-text {
    height:52px;    
    //font-size: 110%;
}
.Info {
    font-size: xx-large;
    font-variant: all-petite-caps;
    margin-top: -28%;

}
.btni {
    margin-top:-53%;
}
.edite {
    width: 7%;
    margin-left: 98%;
    margin-top: -70%;
}
.bordered {
    border-style: solid;
    border-width: 1px;
}
.editP{
    width:15%;
}
.participant{
    width:16%;
}
.card-header.bg-danger {
    background-color:#db7070 !important;
}
</style>