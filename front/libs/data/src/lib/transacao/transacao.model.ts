import { CategoriaModel } from "../categoria";

export enum TipoTransacao {
    Entrada,
    Saida
}

export interface TransacaoModel {
    id: any;
    tipo: any;
    categoria: CategoriaModel,
    categoriaId: string,
    valor: number,
    data: any;
    descricao: string;
}