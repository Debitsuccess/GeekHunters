import { Component, Inject, Injectable } from '@angular/core';
import { Http, RequestMethod, RequestOptions, Headers } from '@angular/http';
import 'rxjs/Rx';

import { Candidate } from './candidate.type';

@Injectable()
export class GeekService {
    public readonly TechnologyList: string[];

    constructor(private http: Http, @Inject('BASE_URL') private originUrl: string) {
        this.TechnologyList = ["Azure", "SQL", "CSharp", "Angular"];
    }

    public getCandidates() {
        return this.http.get(this.originUrl + 'api/Candidate/GetCandidatesAsynch/')
            .map(response => response.json() as Candidate[]);
    }

    public getCandidate(id: string) {
        return this.http.get(this.originUrl + 'api/Candidate/GetCandidateAsynch/' + id)
            .map(response => response.json() as Candidate);
    }

    public updateCandidate(candidate: Candidate) {
        let body = JSON.stringify(candidate);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.originUrl + 'api/Candidate/UpdateAsync/', body, options)
            .map(response => response.text());
    }

    public addCandidate(candidate: Candidate) {
        let body = JSON.stringify(candidate);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.originUrl + 'api/Candidate/AddAsynch/', body, options)
            .map(response => response.text());
        }

    public deleteCandidate(id: string) {
        return this.http.get(this.originUrl + 'api/Candidate/DeleteAsynch/' + id)
            .map(response => response.text());
    }
}
