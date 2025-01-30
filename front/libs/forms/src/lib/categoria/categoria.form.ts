
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { FormGroupBase } from "../form.base";

const fb = new FormBuilder();

export class CategoriaFormGroup extends FormGroupBase {

  get id(): FormControl {
    return this.get('id') as FormControl;
  }

  get nome(): FormControl {
    return this.get('nome') as FormControl;
  }

  get default(): FormControl {
    return this.get('default') as FormControl;
  }


  constructor() {
    super({
      id: fb.control(null),
      nome: fb.control('', Validators.required),
      default: fb.control(false)
    });
  }
}
