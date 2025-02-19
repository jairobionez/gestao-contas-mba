
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { FormGroupBase } from "../form.base";

const fb = new FormBuilder();

export class TransacaoFiltroFormGroup extends FormGroupBase {

  get tipo(): FormControl {
    return this.get('tipo') as FormControl;
  }

  get categoriaId(): FormControl {
    return this.get('categoriaId') as FormControl;
  }

  get dataInicial(): FormControl<Date> {
    return this.get('dataInicial') as FormControl;
  }

  get dataFinal(): FormControl<Date> {
    return this.get('dataFinal') as FormControl;
  }

  constructor() {
    super({
      tipo: fb.control(null),
      categoriaId: fb.control(null),
      valorInicial: fb.control(null),
      valorFinal: fb.control(null),
      dataInicial: fb.control(null),
      dataFinal: fb.control(null),
    });
  }
}
