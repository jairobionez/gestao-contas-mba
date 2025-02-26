import { Component, inject, OnInit } from '@angular/core';
import { CategoriaResponseModel, TransacaoResponseModel } from '@front/data';
import { CreateEditTransacaoComponent } from '../create-edit-transacao/create-edit-transacao.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent, AlertOptions, ModalInfoComponent, ModalInfoModel } from '@front/components';
import { filter, switchMap, take } from 'rxjs';
import { TransacaoService, CategoriaService } from '@front/services';
import { TransacaoFiltroFormGroup } from '@front/forms';

const EXCEL_TYPE =
  'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';

import * as XLSX from 'xlsx';
import * as moment from 'moment';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-lista-transacao',
  templateUrl: 'lista-transacao.component.html',
  styleUrls: ['lista-transacao.component.scss'],
  standalone: false
})
export class ListaTransacaoComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  form: TransacaoFiltroFormGroup;

  displayedColumns: string[] = ['tipo', 'categoria', 'data', 'valor', 'descricao', 'acoes'];

  tiposSelecionados = [0, 1];

  categorias: CategoriaResponseModel[] = [];

  transacoes: TransacaoResponseModel[] = [];

  constructor(
    private categoriaService: CategoriaService,
    private transacaoService: TransacaoService,
  ) {
    this.form = new TransacaoFiltroFormGroup();
  }

  ngOnInit(): void {
    this.categoriaService.get()
      .pipe(take(1))
      .subscribe(data => {
        this.categorias = data;
      });

    this.buscar();
  }

  novaTransacao(): void {
    const ref = this.dialog.open(CreateEditTransacaoComponent, {
      width: '50rem',
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(nova => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Transação alterada',
            status: 'sucesso'
          } as AlertOptions
        });
        this.buscar();
      });
  }

  editarTransacao(transacao: TransacaoResponseModel): void {
    const ref = this.dialog.open(CreateEditTransacaoComponent, {
      width: '50rem',
      data: transacao
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Transação alterada',
            status: 'sucesso'
          } as AlertOptions
        });
        this.buscar();
      });
  }

  removerTransacao(transacaoId: any): void {
    const ref = this.dialog.open(ModalInfoComponent, {
      width: '50rem',
      data: {
        titulo: 'Remover Transação',
        texto: 'Esta ação não pode ser desfeita, deseja confirma-la?',
        btnOk: 'Confirmar',
        btnCancel: 'Voltar'
      } as ModalInfoModel
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data),
        switchMap(data => this.transacaoService.delete(transacaoId)))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Categoria removida',
            status: 'sucesso'
          } as AlertOptions
        });

        this.buscar();
      });
  }

  buscar(): void {
    const { valid, value } = this.form;
    this.transacaoService.getByFilters(value)
      .pipe(take(1))
      .subscribe(data => {
        this.transacoes = data;
      });
  }


  exportarTransacoes(): void {
    const report: any[] = [];

    this.transacoes.forEach((transacao: TransacaoResponseModel) => {
      report.push({
        Tipo: transacao.tipo == 0 ? "Entrada" : "Saída",
        Categoria: transacao.categoria.nome,
        Valor: transacao.valor,
        Data: moment(transacao.data).format('DD/MM/yyyy'),
        Descricacao: transacao.valor
      });
    });


    const worksheet = XLSX.utils.json_to_sheet(report);

    const wscols = [
      { wch: 30 },
      { wch: 30 },
      { wch: 30 },
      { wch: 20 },
      { wch: 50 },
    ];


    worksheet['!cols'] = wscols;

    const workbook = {
      Sheets: { data: worksheet },
      SheetNames: ['data'],
    };

    const headerStyle = {
      font: { bold: true },
    };

    for (const col in worksheet) {
      if (col.startsWith('A')) {
        const cell = worksheet[col];
        cell.s = headerStyle;
      }
    }

    const excelBuffer = XLSX.write(workbook, {
      bookType: 'xlsx',
      type: 'array',
    });

    this.saveAsExcelFile(excelBuffer, `Transações`);
  }

  private saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], {
      type: EXCEL_TYPE,
    });

    FileSaver.saveAs(
      data,
      fileName + '_' + moment().format('DD/MM/yyyy') + EXCEL_EXTENSION
    );
  }
}
