export class Candidate {
    id: string;
    firstName: string;
    lastName: string;
    technologies: string[];

    public constructor(
        fields?: {
            id: string,
            firstName: string,
            lastName: string,
            technologies?: string[]
        }) {
        if (fields)
            Object.assign(this, fields);
    }
}