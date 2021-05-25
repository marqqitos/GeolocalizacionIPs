import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IPDistancia } from 'src/app/models/IPDistancia';
import { IPInfo } from 'src/app/models/IPInfo';
import { InformacionIPService } from 'src/app/services/InformacionIP/informacion-ip.service';

@Component({
  selector: 'app-informacion-ip',
  templateUrl: './informacion-ip.component.html',
  styleUrls: ['./informacion-ip.component.css']
})
export class InformacionIPComponent implements OnInit {
  IP: string;
  infoIP: IPInfo;
  ipDistancia: IPDistancia;
  mensajeDistancia: string;
  buscarDistancia: boolean = false;
  malFormatoIP: boolean = false;
  errorBusquedaDistancia:boolean = false;

  constructor(private ipInfoService: InformacionIPService) { }

  ngOnInit(): void {
  }

  buscarInformacionIP() {
    this.errorBusquedaDistancia = false;
    if(this.validIPaddress(this.IP)) {
      this.malFormatoIP = false;
      this.ipInfoService.getIpInfo(this.IP).subscribe(response => {
        this.infoIP = response;
        this.buscarDistancia = false;
      })
    }
    else {
      this.malFormatoIP = true;
    }
  }

  validIPaddress(ipaddress) {  
    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {  
      return (true)  
    }  
    alert("Ingresaste una IP con mal formato!")  
    return (false)  
  } 


  buscarDistanciaMasLejana() {
    this.setFlagsBusquedaDistancia("mas lejana a Buenos Aires");
    this.ipInfoService.buscarDistanciaMasLejana().subscribe(response => {
      this.ipDistancia = response;
    }, error => {
      this.setFlagsErrorBusquedaDistancia();
    })
  }

  buscarDistanciaMasCercana() {
    this.setFlagsBusquedaDistancia("mas cercana a Buenos Aires");
    this.ipInfoService.buscarDistanciaMasCercana().subscribe(response => {
      this.ipDistancia = response;
    }, error => {
      this.setFlagsErrorBusquedaDistancia();
    })
  }

  buscarDistanciaPromedio() {
    this.setFlagsBusquedaDistancia("promedio");
    this.ipInfoService.buscarDistanciaPromedio().subscribe(response => {
      this.ipDistancia = response;
    }, error => {
      this.setFlagsErrorBusquedaDistancia();
    })
  }

  setFlagsBusquedaDistancia(msjDistancia: string) {
    this.buscarDistancia = true;
    this.errorBusquedaDistancia = false;
    this.mensajeDistancia = msjDistancia;
  }

  setFlagsErrorBusquedaDistancia() {
    this.buscarDistancia = false;
      this.errorBusquedaDistancia = true;
  }

  getHoras() {
    var pipe = new DatePipe('en-US');
    var now = Date.now();
    var horas = "";
    var index = 1;
    
    this.infoIP.franjasHorarias.forEach(franjaUTC => {
      var formattedHora = pipe.transform(now, 'HH:mm:SS', franjaUTC);
      if(this.infoIP.franjasHorarias.length === 1) {
        horas += formattedHora + ' (' + franjaUTC + ')';
      }
      else if(index === this.infoIP.franjasHorarias.length) {
        horas += formattedHora + ' (' + franjaUTC + ')';
      }
      else {
        horas += formattedHora + ' (' + franjaUTC + ') o ';
      }

      index++;
    });
    
    return horas;
  }
}
