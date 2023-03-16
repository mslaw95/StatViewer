<template>
  <div class="container">
    <DatePickersComponent @statistics-fetched="handleStatistics"/>
    <ChartComponent class="chart" :chartData="chartData" :key="chartKey"/>
  </div>
</template>
  
<style>
.container {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.chart {
  margin-top: 20px;
  max-height: 700px ;
  max-width: 1400px ;
  align-items: center;
}
</style>

<script>
import DatePickersComponent from './components/DatePickersComponent.vue'
import ChartComponent from './components/ChartComponent.vue'

export default {
  name: 'App',
  components: {
    DatePickersComponent,
    ChartComponent
  },
  data() {
    return {
      chartKey: 0,
      chartData: {
        labels: [],
        datasets: []
      },
    }
  },
  methods: {
    forceRerender() {
      this.chartKey += 1;
    },
    handleStatistics(statistics) {
      if (!statistics.data || !statistics.data.statistics || statistics.data.statistics.length === 0) {
        this.chartData.labels = [''],
        this.chartData.datasets = ['']
      }

      const labels = statistics.data.statistics.map((stat) => stat.date);
      const values = statistics.data.statistics.map((stat) => stat.count);

      this.chartData.labels = labels,
      this.chartData.datasets = [{
          label: 'Count',
          backgroundColor: 'blue',
          data: values,
      }]
      this.forceRerender();
    }
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
