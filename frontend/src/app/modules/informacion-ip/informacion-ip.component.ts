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

  constructor(private ipInfoService: InformacionIPService) { }

  ngOnInit(): void {
  }

  buscarInformacionIP() {
    if(this.validIPaddress) {
      this.ipInfoService.getIpInfo(this.IP).subscribe(response => {
        this.infoIP = response;
      })
    }
    else {
      
    }
  }

  validIPaddress(ipaddress) {  
    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {  
      return (true)  
    }  
    alert("You have entered an invalid IP address!")  
    return (false)  
  } 

  buscarDistanciaMasLejana() {
    this.ipInfoService.buscarDistanciaMasLejana().subscribe(response => {
      this.ipDistancia = response;
      this.mensajeDistancia = "mas lejana a Buenos Aires";
    })
  }

  buscarDistanciaMasCercana() {
    this.ipInfoService.buscarDistanciaMasCercana().subscribe(response => {
      this.ipDistancia = response;
      this.mensajeDistancia = "mas cercana a Buenos Aires";
    })
  }

  buscarDistanciaPromedio() {
    this.ipInfoService.buscarDistanciaPromedio().subscribe(response => {
      this.ipDistancia = response;
      this.mensajeDistancia = "promedio";
    })
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
