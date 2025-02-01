import { Component, inject, OnInit } from '@angular/core';
import { CategoriaResponseModel, TipoTransacao, TransacaoResponseModel } from '@front/data';
import { CreateEditTransacaoComponent } from '../create-edit-transacao/create-edit-transacao.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent, AlertOptions, ModalInfoComponent, ModalInfoModel } from '@front/components';
import { filter, take } from 'rxjs';
import { TransacaoService, CategoriaService, HeaderService } from '@front/services';

@Component({
  selector: 'app-lista-transacao',
  templateUrl: 'lista-transacao.component.html',
  styleUrls: ['lista-transacao.component.scss'],
  standalone: false
})
export class ListaTransacaoComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  displayedColumns: string[] = ['tipo', 'categoria', 'valor', 'descricao', 'acoes'];

  tiposSelecionados = [0,1];

  categorias: CategoriaResponseModel[] = [];

  transacoes: TransacaoResponseModel[] = [];

  constructor(
    private headerService: HeaderService,
    private categoriaService: CategoriaService,
    private transacaoService: TransacaoService,
  ) {
    this.headerService.alterarTitulo('Transações');

  }   

  ngOnInit(): void { 
    this.categoriaService.get()
      .pipe(take(1))
      .subscribe(data => {
        this.categorias = data;
      });

    this.transacaoService.get()
    .pipe(take(1))
      .subscribe(data => {        
        this.transacoes = data;
      });
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
        console.log(nova);
        this.transacaoService.post(nova)
              .pipe()
              .subscribe(_ => {
                this._snackBar.openFromComponent(AlertComponent, {
                  duration: 5000,
                  data: {
                    title: 'Sucesso!',
                    subtitle: 'Transação criada',
                    status: 'sucesso'
                  } as AlertOptions
                });
              });
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
          });
  }
  
  removerTransacao(): void {
    const ref = this.dialog.open(ModalInfoComponent, {
          width: '50rem',
          data: {
            titulo: 'Remover transação',
            texto: 'Esta ação não pode ser desfeita, deseja confirma-la?',
            btnOk: 'Confirmar',
            btnCancel: 'Voltar'
          } as ModalInfoModel
        });
  }
}
