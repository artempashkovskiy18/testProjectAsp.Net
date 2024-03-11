export class UrlModel {
  constructor(id: number, fullUrl: string, shortUrl: string, creator: string, creationDate: Date) {
    this.id = id;
    this.fullUrl = fullUrl;
    this.shortUrl = shortUrl;
    this.creator = creator;
    this.creationDate = creationDate;
  }

  readonly id: number;
  fullUrl: string;
  shortUrl: string;
  creator: string;
  creationDate: Date;
}
