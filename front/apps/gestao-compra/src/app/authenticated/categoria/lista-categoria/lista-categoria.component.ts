import { Component, OnInit } from '@angular/core';
import { CategoriaResponseModel } from '@front/data';
import { CategoriaService, HeaderService } from '@front/services';
import { take } from 'rxjs';


@Component({
  selector: 'app-lista-categoria',
  templateUrl: 'lista-categoria.component.html',
  styleUrls: ['lista-categoria.component.scss'],
  standalone: false
})
export class ListaCategoriaComponent implements OnInit {

  displayedColumns: string[] = ['nome', 'ativo', 'acoes'];
  categorias: CategoriaResponseModel[] = [
    {
      nome: 'teste',
      ativo: true,
      default: true,
      id: '123'
    }
  ];


  constructor(
    private headerService: HeaderService,
    private categoriaService: CategoriaService
  ) {
    this.headerService.alterarTitulo('Categorias');
  }

  ngOnInit() {
    this.categoriaService.get()
      .pipe(take(1))
      .subscribe(data => {
        debugger;
        this.categorias = data;
      });
  }

}
