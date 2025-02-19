import { CategoriaResponseModel } from "../categoria";



export interface TransacaoResponseModel {
    id: any;
    tipo: any;
    categoria: CategoriaResponseModel,
    valor: number,
    data: Date;
    descricao: string;
  }