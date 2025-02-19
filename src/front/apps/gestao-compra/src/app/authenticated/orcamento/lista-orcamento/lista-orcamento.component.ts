import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent, AlertOptions, ModalInfoComponent, ModalInfoModel } from '@front/components';
import { OrcamentoResponseModel } from '@front/data';
import { OrcamentoService } from '@front/services';
import { filter, switchMap, take } from 'rxjs';
import { CreateEditOrcamentoComponent } from '../create-edit-orcamento/create-edit-orcamento.component';

@Component({
  selector: 'app-lista-orcamento',
  templateUrl: 'lista-orcamento.component.html',
  styleUrls: ['./lista-orcamento.component.scss'],
  standalone: false
})

export class ListaOrcamentoComponent implements OnInit {

  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);
  orcamentos: OrcamentoResponseModel[] = [];
  displayedColumns: string[] = ['categoria', 'usuario', 'limite', 'acoes'];

  constructor(
    private orcamentoService: OrcamentoService,
  ) { }

  ngOnInit() {
    this.getOrcamentos();
  }

  getOrcamentos(): void {
    this.orcamentoService.get()
      .pipe(take(1))
      .subscribe(data => {
        this.orcamentos = data;
      });
  }

  novoOrcamento(): void {
    const ref = this.dialog.open(CreateEditOrcamentoComponent, {
      width: '50rem',
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Categoria criada',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getOrcamentos();
      });
  }

  editarOrcamento(orcamento: OrcamentoResponseModel): void {
    const ref = this.dialog.open(CreateEditOrcamentoComponent, {
      width: '50rem',
      data: orcamento
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Orçamento alterado',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getOrcamentos();
      });
  }

  removerOrcamento(orcamentoId: any): void {
    const ref = this.dialog.open(ModalInfoComponent, {
      width: '50rem',
      data: {
        titulo: 'Remover orçamento',
        texto: 'Esta ação não pode ser desfeita, deseja confirma-la?',
        btnOk: 'Confirmar',
        btnCancel: 'Voltar'
      } as ModalInfoModel
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data),
        switchMap(data => this.orcamentoService.delete(orcamentoId)))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Orçamento removido',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getOrcamentos();
      });
  }
}
