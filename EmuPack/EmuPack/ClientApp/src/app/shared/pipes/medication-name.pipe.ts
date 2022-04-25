import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'medicationName'
})
export class MedicationNamePipe implements PipeTransform {

  transform(medicationName: string): string {
    return medicationName.trim();
  }

}
