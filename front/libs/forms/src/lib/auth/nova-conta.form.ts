
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { FormGroupBase } from "../form.base";
import { EnderecoFormGroup } from "../endereco.form";
import { passwordMatchValidator } from "../validators";
import { NgBrazilValidators } from 'ng-brazil'

const fb = new FormBuilder();

export class NovaContaFormGroup extends FormGroupBase {

  get nome(): FormControl {
    return this.get('nome') as FormControl;
  }

  get sobrenome(): FormControl {
    return this.get('sobrenome') as FormControl;
  }

  get cpf(): FormControl {
    return this.get('cpf') as FormControl;
  }

  get email(): FormControl {
    return this.get('email') as FormControl;
  }

  get senha(): FormControl {
    return this.get('senha') as FormControl;
  }

  get confirmarSenha(): FormControl {
    return this.get('confirmarSenha') as FormControl;
  }

  get endereco(): FormGroup {
    return this.get('endereco') as FormGroup;
  }

  constructor() {
    super({
      nome: fb.control('', [Validators.required]),
      sobrenome: fb.control('', [Validators.required]),
      cpf: fb.control('', [Validators.required, NgBrazilValidators.cpf]),
      endereco: new EnderecoFormGroup,
      email: fb.control('', [Validators.required, Validators.email]),
      senha: fb.control('', [Validators.required]),
      confirmarSenha: fb.control('', [Validators.required])
    }, {
      validators: passwordMatchValidator('senha', 'confirmarSenha')
    });
  }
}
