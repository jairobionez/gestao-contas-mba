
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { FormGroupBase } from "../form.base";

const fb = new FormBuilder();

export class OrcamentoFormGroup extends FormGroupBase {

  get id(): FormControl {
    return this.get('id') as FormControl;
  }

  get limite(): FormControl {
    return this.get('limite') as FormControl;
  }

  get categoriaId(): FormControl {
    return this.get('categoriaId') as FormControl;
  }

  // get usuarioId(): FormControl {
  //   return this.get('usuarioId') as FormControl;
  // }


  constructor() {
    super({
      id: fb.control(null),
      limite: fb.control('', [Validators.required, Validators.min(0)]),
      categoriaId: fb.control(null, Validators.required),
      // usuarioId: fb.control(null, Validators.required)
    });
  }
}
