export enum TipoTransacao {
    Entrada,
    Saida
}

export interface TransacaoModel {
    tipoTransacao: any;
    categoriaId: any,
    valor: number,
    data: Date;
    descricao: string;
}