import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { DashboardFiltroFormGroup } from '@front/forms';
import { DashboardService } from '@front/services';
import { EChartsOption } from 'echarts';
import { take } from 'rxjs';

@Component({
    selector: 'app-dashboard',
    standalone: false,
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {

  options!: EChartsOption;
  
  form: DashboardFiltroFormGroup = new DashboardFiltroFormGroup();

  totalEntradas = 0;
  totalSaidas = 0;
  saldoTotal = 0;
  mediaGastosDiarios = 0;

  private dashService = inject(DashboardService);

  constructor(private cd: ChangeDetectorRef)
  {
    const now = new Date();
    const primeiroDia = new Date(now.getFullYear(), now.getMonth(), 1);
    const ultimoDia = new Date(now.getFullYear(), now.getMonth() + 1, 0);

    this.form.dataInicial.setValue(primeiroDia);
    this.form.dataFinal.setValue(ultimoDia);
  }

  ngOnInit() {
  this.getIndicadores();
  }

  getIndicadores(){
    this.dashService.indicadores(this.form.value)
    .pipe(take(1))
    .subscribe({
      next: (res: any) => {
        this.totalEntradas = res.data.totalEntradas;
        this.totalSaidas = res.data.totalSaidas;
        this.mediaGastosDiarios = res.data.mediaGastosDiarios;
        this.saldoTotal = this.totalEntradas - this.totalSaidas;

        const valores = res.data.categoriasGasto.map((x: any) => x.totalGasto).reverse();
        const nomes = res.data.categoriasGasto.map((x: any) => x.nomeCategoria).reverse();

        this.buildDashboardOption(nomes, valores);
      },
      error: (err: any) => {

      }
    });
  }

  buildDashboardOption(categoria: string[], valores: number[]) {
    // Função para formatar valores como dinheiro (ex: 150,00)
    const formatCurrency = (value: number): string => {
      return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    };
  
    this.options = {
      textStyle: {
        fontFamily: 'Poppins'
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'cross',
          label: {
            backgroundColor: '#6a7985',
            fontFamily: 'Poppins',
          },
        },
      },
      grid: {
        left: '3%',
        right: '3%',
        top: '10%',
        bottom: '10%',
        containLabel: true
      },
      xAxis: [
        {
          type: 'value',
          position: 'bottom',
          offset: 20,
          axisLabel: {
            formatter: (value: number) => formatCurrency(value),
            fontFamily: 'Poppins',
            color: '#000',
            fontWeight: 500,
            align: 'right',
            rotate: 0,
            overflow: 'truncate',
          },
          splitLine: {
            lineStyle: {
              color: '#646CFF4D',
              type: 'dashed',
            },
            show: true
          },
          interval: Math.max(...valores) / 4,
          max: Math.max(...valores)
        },
      ],
      yAxis: [
        {
          type: 'category',
          data: [...categoria],
          position: 'left',
          offset: 2,
          axisLine: {
            lineStyle: {
              color: 'transparent'
            }
          },
          axisLabel: {
            formatter: (value: string) => {
              return value;
            },
            fontFamily: 'Poppins',
            color: '#000',
            fontWeight: 500
          },
        },
      ],
      series: [
        {
          name: 'Valor',
          type: 'bar',
          stack: 'counts',
          data: valores.map((valor, index) => ({
            value: valor,
            itemStyle: {
              color: index === 0 ? '#BF545B' : index === 1 ? '#FFA07A' : '#267FA4'
            }
          })),
          barMaxWidth: 24,
          itemStyle: {
            borderRadius: [0, 5, 5, 0],
          },
          animation: true,
          animationDuration: 1000
        }
      ],
      barBorderRadius: 5,
      backgroundColor: '#E7F8FF',
    };
    this.cd.detectChanges();
  }
}