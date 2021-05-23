import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { IPDistancia } from 'src/app/models/IPDistancia';
import { IPInfo } from 'src/app/models/IPInfo';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InformacionIPService {
  commonRoot = 'ipinfo/';

  constructor(private http: HttpClient) {}

  getIpInfo(ip: string) {
    return this.http.get<IPInfo>(environment.baseUrl + this.commonRoot + ip);
  }

  buscarDistanciaMasLejana() {
    return this.http.get<IPDistancia>(environment.baseUrl + this.commonRoot + "distanciaMasLejanaABuenosAires");
  }

  buscarDistanciaMasCercana() {
    return this.http.get<IPDistancia>(environment.baseUrl + this.commonRoot + "distanciaMasCercanaABuenosAires");
  }

  buscarDistanciaPromedio() {
    return this.http.get<IPDistancia>(environment.baseUrl + this.commonRoot + "distanciaPromedio");
  }
}
