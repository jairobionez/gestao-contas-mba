import { FormBuilder, FormControl } from "@angular/forms";
import { FormGroupBase } from "@front/forms";

const fb = new FormBuilder();

export class DashboardFiltroFormGroup extends FormGroupBase {

  get dataInicial(): FormControl<Date> {
    return this.get('dataInicial') as FormControl;
  }

  get dataFinal(): FormControl<Date> {
    return this.get('dataFinal') as FormControl;
  }

  constructor() {
    super({
      dataInicial: fb.control(null),
      dataFinal: fb.control(null),
    });
  }
}
