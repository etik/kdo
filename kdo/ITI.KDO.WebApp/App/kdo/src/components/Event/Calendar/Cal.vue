<template>
    <div id="app"
         class="demo">
        <h1 class="demo-title">Calendar</h1>
        <div class="demo-container">
            <schedule-Calendar :originData="eventList2"></schedule-Calendar>
        </div>
    </div>
</template>
<script>
import scheduleCalendar from './scheduleCalendar'
import EventApiService from '../../../services/EventApiService'
import UserApiService from '../../../services/UserApiService'
import AuthService from '../../../services/AuthService'

export default {
    name: 'app',
    components: {
        scheduleCalendar
    },

    data() {
        return {
            eventList2: [
                {
                    eventId: 111,
                    userId: 1,
                    dates: '2018-01-15',
                    eventName: "event1",
                    descriptions: "blabla"
                },
                {
                    eventId: 222,
                    userId: 2,
                    dates: '2018-01-15',
                    eventName: "event2",
                    descriptions: "balblabal"
                },
            ],
            user: Object,
            eventList: Array
        }
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },
    
    methods: {
        async refreshList() {
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            console.log(this.eventList);
        }
    }
}
</script>
<style lang="less">
html {
    background: #eee
}

body,
html {
    height: 100%;
    overflow: hidden;
}

body {
    margin: 0;
}

.demo {
    display: flex;
    flex-direction: column;
    height: 100%;
    padding: 0 30px 30px;
    box-sizing: border-box
}

.demo-title {
    text-align: center
}

.demo-container {
    flex: 1
}
</style>