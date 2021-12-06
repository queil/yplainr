FROM node

ARG UID
ARG GID
USER $UID
WORKDIR /app

RUN yarn init -y && \
    yarn && \
    yarn set version stable && \
    yarn add ts2fable

COPY ts2fable.sh ./ts2fable.sh
USER root
RUN chmod +x ./ts2fable.sh && chown $UID:$GID /app
USER $UID

ENTRYPOINT ["/app/ts2fable.sh"]
