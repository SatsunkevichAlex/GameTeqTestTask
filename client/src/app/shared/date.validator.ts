import { AbstractControl, ValidationErrors } from '@angular/forms';

export function dateVaidator(AC: AbstractControl): ValidationErrors | null {
  let value = new Date(AC.value);
  let currentDate = new Date();
  if (AC && value && value > currentDate) {
    return { 'invalidDate': true };
  }
  return null;
}
