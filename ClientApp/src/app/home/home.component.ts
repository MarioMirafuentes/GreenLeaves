import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";
import { DatePipe } from '@angular/common';
import { formatDate } from '@angular/common';
import localeFr from '@angular/common/locales/es-MX';
import { registerLocaleData } from '@angular/common';
import { ServicesgreenService } from '../services/servicesgreen.service';

import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})


export class HomeComponent {
  pipe = new DatePipe('es-MX');
  FormContacto: FormGroup;
  siteKey = '6LcqBeEiAAAAANVCJOZBvYua9BlnfzdtkJhRR2kb'
  ////////
  searchText: any;
  names: any;
  mailValid: boolean = false;
  telValid: boolean = false;
  dateValid: boolean = false;
  ///////
  cpacha: string;
  email: string;
  searchActivate: boolean = false;
  constructor(private greenservices: ServicesgreenService,
  ) {

    this.cpacha = '';
    this.email = 'jmacsm@gmail.com'
    this.FormContacto = new FormGroup({
      nombre: new FormControl("", [
        Validators.required,
        Validators.maxLength(250)]),
      email: new FormControl("", [
        Validators.required,

        Validators.maxLength(100)]),
      telefono: new FormControl("", [
        Validators.required,
        Validators.minLength(16),
        Validators.maxLength(100)]),
      fecha: new FormControl("", [
        Validators.required,
        Validators.maxLength(100)]),
      ciudad: new FormControl("", [
        Validators.required,
        Validators.maxLength(100)]),



    });

  }
  ngOnInit() { }

  SelectDate(date) {
    let date1 = date.value;
    this.dateValid = this.IsvalidDate(date1);
    registerLocaleData(localeFr, 'MX');
    let newdate = formatDate(date1, 'dd/MMMM/yyyy', 'Mx');
    date1 = newdate.split('/');

    this.FormContacto.controls["fecha"].setValue(date1[0] + ' de ' + date1[1] + ' del ' + date1[2]);

  }
  Search(city) {
    if (city.value.length > 3) {
      this.greenservices.GetSearchCity(city.value).subscribe(res => {

        this.searchActivate = true;
        this.names = res
      });
    }
  }
  SelectCity(name) {
    console.log(name);
    this.FormContacto.controls["ciudad"].setValue(
      name.name + ", " + name.adminName1 + ", " + name.countryName
    );
    this.searchActivate = false;

  }
  ValidEmail(email) {

    this.esEmailValido(email.value)
  }
  esEmailValido(email: string): boolean {
    let mailValido = false;
    'use strict';

    var EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (email.match(EMAIL_REGEX)) {
      mailValido = true;
      this.mailValid = true;
    }
    else {
      this.mailValid = false;
    }
    return mailValido;
  }


  esTelValido(tel: string): boolean {
    let telValido = false;
    if (tel.length >= 13) {
      telValido = true;
    }

    return telValido;
  }

  format(value, pattern): void {
    value = value + '000'
    if (this.esTelValido(value)) {

      pattern = pattern.substring(0, value.length);
      var i = 0,
        v = value.toString();
      let final = pattern.replace(/#/g, _ => v[i++]);
      this.FormContacto.controls["telefono"].setValue(final);
      this.telValid = true;
      //this.numero = pattern.replace(/#/g, _ => v[i++]);
    } else {
      this.telValid = false;
    }
  }

  IsvalidDate(date: Date): boolean {
    let valid = false;

    let anioactual = new Date("2022");
    let anioquery = new Date(date);
    var result = anioactual.getFullYear() - anioquery.getFullYear()
    if (result < 100) {
      valid = true;
    }
    console.log(valid);
    return valid;
  }

  SaveForm() {
    this.greenservices.SetSaveContact(this.FormContacto.value).subscribe(res => {
      if (res = 1) {
        window.alert("El formulario fue enviado correctamente");
        this.FormContacto.reset();
        this.telValid = false;
        this.mailValid = false;
        this.dateValid = false;

      } else {
        window.alert("El formulario no se genero correctamenete, intente de nuevo.");
      }
    });
  }

}
