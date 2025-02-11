
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { FormGroupBase } from "../form.base";

const fb = new FormBuilder();

export class TransacaoFormGroup extends FormGroupBase {

  get id(): FormControl {
    return this.get('id') as FormControl;
  }

  get tipoTransacao(): FormControl {
    return this.get('tipoTransacao') as FormControl;
  }

  get categoriaId(): FormControl {
    return this.get('categoriaId') as FormControl;
  }

  get valor(): FormControl {
    return this.get('valor') as FormControl;
  }

  get data(): FormControl {
    return this.get('data') as FormControl;
  }

  get descricao(): FormControl {
    return this.get('descricao') as FormControl;
  }

  constructor() {
    super({
      id: fb.control(null),
      tipoTransacao: fb.control(0, Validators.required),
      categoriaId: fb.control('', Validators.required),
      valor: fb.control(0, Validators.required),
      data: fb.control('' , Validators.required),
      descricao: fb.control('')
    });
  }
}
