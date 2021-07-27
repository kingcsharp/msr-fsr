FROM nginx

COPY --from=gik25138.live.dynatrace.com/linux/oneagent-codemodules-musl:dotnet / /
ENV LD_PRELOAD /opt/dynatrace/oneagent/agent/lib64/liboneagentproc.so

ARG NGINX_CONF
RUN echo $NGINX_CONF
ENV FILE="${NGINX_CONF}.nginx.conf"
COPY $FILE /etc/nginx/nginx.conf

