<template>
    <div id="app"
         class="demo">
        <h1 class="demo-title">Vue Schedule Calendar</h1>
        <div class="demo-container">
            <schedule-calendar :originData="data"></schedule-calendar>
        </div>
    </div>
</template>
<script>
import scheduleCalendar from './scheduleCalendar'
export default {
    name: 'app',
    components: {
        scheduleCalendar
    },
    data() {
        return {
            data: [
                {
                    id: 111,
                    date: '2018-01-06',
                    text: 'event1'
                },
                {
                    id: 222,
                    date: '2018-01-16',
                    text: 'event2'
                },
                {
                    id: 333,
                    date: '2017-04-03',
                    text: 'event3'
                },
                {
                    id: 444,
                    date: '2017-04-03',
                    text: 'event4'
                },
                {
                    id: 555,
                    date: '2017-04-03',
                    text: 'event5'
                },
                {
                    id: 666,
                    date: '2017-04-09',
                    text: 'event6'
                },
                {
                    id: 777,
                    date: '2017-04-09',
                    text: 'event7'
                },
                {
                    id: 888,
                    date: '2017-04-09',
                    text: 'event8'
                },
                {
                    id: 999,
                    date: '2017-04-09',
                    text: 'event9'
                },
                {
                    id: 1010,
                    date: '2017-04-09',
                    text: 'event10'
                }
            ]
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